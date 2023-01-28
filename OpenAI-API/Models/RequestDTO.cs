namespace OpenAI_API.Models
{
    public class RequestDTO
    {
        public string model {  get; set; }
        public string prompt { get; set; } 
        public float temperature { get; set; }
        public int max_tokens { get; set; }
    }
}