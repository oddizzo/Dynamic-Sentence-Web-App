namespace Dynamic_Sentence_Web_App.Models
{
    public class WordSentence
    {
        public int WordUnitId { get; set; }
        public WordUnit WordUnit { get; set; }
        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }
    }
}
