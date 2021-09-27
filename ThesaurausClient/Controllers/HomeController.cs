using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ThesaurausClient.Helpers;
using ThesaurausClient.Models;
using ThesaurausLibrary;

namespace ThesaurausClient.Controllers
{
    public class HomeController : Controller
    {
        HomeHelper helper = new HomeHelper();
        StringLibrary stringLibrary = new StringLibrary();

        /// <summary>
        /// Get all synonmys for the word
        /// </summary>
        /// <param name="keyword">Word</param>
        /// <returns>Synonyms</returns>
        [HttpGet]
        [Route("api/get/{keyword}")]
        public IActionResult GetSynonyms(string keyword)
        {
            List<string> synonymsList = stringLibrary.getSynonyms(keyword);
            int count = synonymsList != null ? synonymsList.Count : 0;
            if (count > 0)
            {
                var responseObj = new
                {
                    synonyms = string.Join(',', synonymsList)
                };
                return new OkObjectResult(responseObj);
            }
            else
                return new NotFoundObjectResult(null);
           
        }


        /// <summary>
        /// Get all words in the Thesauraus library
        /// </summary>
        /// <returns>Words and synonyms</returns>
        [HttpGet]
        [Route("api/getallwords")]
        public IActionResult GetAllWords()
        {
            Dictionary<string,List<string>> wordsDict = stringLibrary.getAllWords();
            List<WordSynonyms> wordsList = helper.GetWordSynonyms(wordsDict);
            int count = wordsList.Count;
            if (count > 0)
            {
                var responseObj = new
                {
                    words = wordsList,
                };
                return new OkObjectResult(responseObj);
            }
            else
                return new NotFoundObjectResult(null);
           
        }

        /// <summary>
        /// Add word and synonyms to library
        /// </summary>
        /// <param name="obj">Object that has word and synonyms</param>
        /// <returns>Created status if word and synonym added succesfuly </returns>
        [HttpPost]
        [Route("api/postwordsynonyms")]
        public IActionResult Post([FromBody]WordSynonyms obj)
        {
            bool isSaveSuccess = stringLibrary.AddSynonym(obj.word, obj.synonymsList);
            
            if (isSaveSuccess)
            {
                var responseObj = new
                {
                    msg = "Saved successfully"
                };
                return new CreatedResult("", responseObj);
               
            }
            else
            {
                var responseObj = new
                {
                    msg = "Word not saved"
                };
                return new BadRequestObjectResult(responseObj);
            }

        }
    }
}
