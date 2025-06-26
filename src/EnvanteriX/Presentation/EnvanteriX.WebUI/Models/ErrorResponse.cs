namespace EnvanteriX.WebUI.Models
{
    public class ErrorResponse
    {
        public Dictionary<string, List<string>> errors { get; set; } = new Dictionary<string, List<string>>();
        public List<string> Errors { get; set; } = new List<string>();
        public int status { get; set; }
    }
}
