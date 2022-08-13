using System.Collections.Generic; 

namespace Dynamic_Sentence_Web_App.Models
{
    public class WordUnit
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int WordTypeId { get; set; }
        public virtual WordType WordType { get; set; }
    }
}
