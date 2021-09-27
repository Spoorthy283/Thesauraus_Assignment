using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ThesaurausClient.Controllers;
using ThesaurausClient.Models;

namespace NUnitTestProject
{
    public class Tests
    {

        /// <summary>
        /// Test by posting data
        /// </summary>
        [Test]
        public void TestPost()
        {
            HomeController controller = new HomeController();
            WordSynonyms wordSynonyms = new WordSynonyms();
            wordSynonyms.word = "test";
            wordSynonyms.synonymsList = new List<string>() { "trial","try" };
            ObjectResult response = (ObjectResult)controller.Post(wordSynonyms);

            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Test with empty string
        /// </summary>
        [Test]
        public void TestPostEmptyString()
        {
            HomeController controller = new HomeController();
            WordSynonyms wordSynonyms = new WordSynonyms();
            wordSynonyms.word = "experiment";
            wordSynonyms.synonymsList = new List<string>() { "" };
            ObjectResult response = (ObjectResult)controller.Post(wordSynonyms);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Get synonyms test
        /// </summary>
        [Test]
        public void TestGet()
        {
            HomeController controller = new HomeController();
            WordSynonyms wordSynonyms = new WordSynonyms();
            wordSynonyms.word = "check";
            wordSynonyms.synonymsList = new List<string>() { "experiment" };

            controller.Post(wordSynonyms);
            ObjectResult response = (ObjectResult)controller.GetSynonyms("check");
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var obj = response.Value;
            string synonyms = obj != null ? (string)obj.GetType().GetProperty("synonyms")?.GetValue(obj, null) : "";
            Assert.AreEqual(true, synonyms.Contains("experiment"));
        }


        /// <summary>
        /// Get all words 
        /// </summary>
        [Test]
        public void TestGetAllWords()
        {
            HomeController controller = new HomeController();
            WordSynonyms wordSynonyms = new WordSynonyms();
            wordSynonyms.word = "assessment";
            wordSynonyms.synonymsList = new List<string>() { "exam","analysis" };
            controller.Post(wordSynonyms);

            WordSynonyms wordSynonyms1 = new WordSynonyms();
            wordSynonyms1.word = "evaluation";
            wordSynonyms1.synonymsList = new List<string>() { "inspection" };
            controller.Post(wordSynonyms1);

            ObjectResult response = (ObjectResult)controller.GetAllWords();
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            var obj = response.Value;
            List<WordSynonyms> words = obj != null ? (List<WordSynonyms>)obj.GetType().GetProperty("words")?.GetValue(obj, null) : null;

            WordSynonyms word1 = words.Where(x => x.word == "assessment" && x.synonyms.Contains("analysis")).FirstOrDefault();
            Assert.AreEqual(true, (word1 != null));

            WordSynonyms word2 = words.Where(x => x.word == "evaluation" && (x.synonyms.Contains("inspection"))).FirstOrDefault();
            Assert.AreEqual(true, (word1 != null));

        }




    }
}