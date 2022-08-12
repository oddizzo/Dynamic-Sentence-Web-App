using System.Collections.Generic;

namespace Dynamic_Sentence_Web_App.Models
{
    public class Sentence
    {
        public int Id { get; set; }
        public List<WordUnit> Words { get; set; }
    }
}
