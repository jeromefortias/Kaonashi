namespace Nexai.Kaonashi.Core.Models
{
    public class Config
    {
        #region "Common"
        public string AppName { get; set; } = "APPx";
        public string? ProjectName { get; set; } = "common";
        public string? Language { get; set; } = "*";
        public string? Category { get; set; }
        public string AppDescription { get; set; } = "Description of the application ";
        public string AppDocumentationUrl { get; set; } = @"https://www.nexai.net";
        #endregion

        #region "Parameters for servers"
        public ESSServerSettings? DataRepository { get; set; }
        
        #endregion
    }
}
