using Microsoft.AspNetCore.Mvc;


namespace OpenAI_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationsController : ControllerBase
    {
        private readonly ILogger<CommunicationsController> _logger;
        private readonly IConfiguration _configuration;

        private HttpClient httpClient;

        public CommunicationsController(ILogger<CommunicationsController> logger, HttpClient client, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            httpClient = client;
        }

        [HttpGet(Name = "TalkToGPT")]
        public async Task<string> Get()
        {
            string uri = "https://api.openai.com/v1/completions";
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);
            //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            //var request = new HttpContent();
            var content = new JsonContent();
            var response = await httpClient.PostAsync(uri, msgContent);

            var headers = HttpContext.Request.Headers;
            headers.Add("Content-Type", "application/json");
            headers.Authorization = = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);
            headers.

            return response.ToString();
        }
    }
}