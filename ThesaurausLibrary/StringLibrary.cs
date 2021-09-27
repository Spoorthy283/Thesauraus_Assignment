using System.Collections.Generic;
using System.Linq;

namespace ThesaurausLibrary
{
    public class StringLibrary
    {

        SqliteConnectionn obj = new SqliteConnectionn();

        /// <summary>
        /// Add word and synonyms
        /// </summary>
        /// <param name="keyword">word</param>
        /// <param name="synonyms">synonyms</param>
        /// <returns></returns>
        public bool AddSynonym(string keyword, List<string> synonyms)
        {
            if (string.IsNullOrWhiteSpace(keyword) || synonyms == null || synonyms.Count == 0)
                return false;

            List<string> filteredSynonyms = synonyms.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (filteredSynonyms.Count == 0)
                return false;

            bool isSuccess = obj.AddWord(keyword, filteredSynonyms);
            return isSuccess;
        }

        /// <summary>
        /// Get synonyms for word
        /// </summary>
        /// <param name="keyword">word</param>
        /// <returns>synonyms</returns>
        public List<string> getSynonyms(string keyword)
        {

            List<string> synonyms = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                synonyms = obj.getSynonyms(keyword);
            }
            return synonyms;
        }

        /// <summary>
        /// get all words and synonyms
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> getAllWords()
        {
            Dictionary<string, List<string>> dict = obj.getAllWords();
            return dict;
        }
    }
}
