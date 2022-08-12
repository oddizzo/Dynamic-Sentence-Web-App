using System.Collections.Generic;

namespace Dynamic_Sentence_Web_App.Models
{
    public class Sentence
    {
        public int Id { get; set; }
        public ICollection<WordSentence> WordSentences { get; set; }
    }
}
