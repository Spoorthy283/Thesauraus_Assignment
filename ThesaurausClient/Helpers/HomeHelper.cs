using System.Collections.Generic;
using ThesaurausClient.Models;

namespace ThesaurausClient.Helpers
{
    public class HomeHelper
    {
        /// <summary>
        /// Get object for words and synonyms
        /// </summary>
        /// <param name="dict">Dictionary that has words and list of synonyms</param>
        /// <returns>WordSynonyms object</returns>
        public List<WordSynonyms> GetWordSynonyms(Dictionary<string, List<string>> dict)
        {
            List<WordSynonyms> wordSynonymList = new List<WordSynonyms>();
            foreach(var item in dict)
            {
                WordSynonyms wordSynonyms = GetWordSynonymsObj(item.Key, item.Value);
                wordSynonymList.Add(wordSynonyms);
            }

            return wordSynonymList;
        }

        /// <summary>
        /// Get WordSynonyms object
        /// </summary>
        /// <param name="key">word</param>
        /// <param name="value">synonms</param>
        /// <returns>WordSynonyms object</returns>
        private WordSynonyms GetWordSynonymsObj(string key, List<string> value)
        {
            WordSynonyms wordSynonyms = new WordSynonyms();
            wordSynonyms.word = key;
            if (value.Count > 0)
            {
                wordSynonyms.synonyms = string.Join(',', value);
            }
            else
            {
                wordSynonyms.synonyms = "";
            }
            return wordSynonyms;
        }
    }
}
