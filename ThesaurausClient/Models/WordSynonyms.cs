using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesaurausClient.Models
{
    public class WordSynonyms
    {
        public string word { get; set; }
        public string synonyms { get; set; }
        public List<string> synonymsList { get; set; }
    }
}
