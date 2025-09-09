namespace JsonAnalyzzzer.Models
{
    [GenerateSerializer]
    [Alias("JsonAnalyzzzer.Models.Dialog")]
    public class Dialog
    {
        [Id(0)]
        public string Prompt { get; set; }
        [Id(1)]
        public string Completion { get; set; }
        [Id(2)]
        public List<string> Tags { get; set; }
        [Id(3)]
        public List<string> ProcessingAction { get; set; }
    }
}
