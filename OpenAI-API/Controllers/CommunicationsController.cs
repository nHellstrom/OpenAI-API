using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using OpenAI_API.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

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
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);

            RequestDTO request = new RequestDTO { model = "text-davinci-003", prompt = "Please motivate me, I am depressed from programming", temperature = 0.4F, max_tokens = 12 };

            var msgJson = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(uri, msgJson);

            //return JsonSerializer.Serialize(request);

            var stringResponse = await response.Content.ReadAsStringAsync();
            //var streamResponse = await response.Content.ReadAsStringAsync(); '
            //return JsonSerializer.Deserialize<ResponseDTO>(streamResponse);
            return stringResponse;
        }
    }
}