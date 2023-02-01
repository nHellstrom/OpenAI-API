using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using OpenAI_API.Models;
using System;
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

        [HttpPost(Name = "TalkToGPT")]
        public async Task<Stream> Get([FromBody] CharacterPrompt charData)
        {
            string uri = "https://api.openai.com/v1/completions";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);

            RequestDTO request = new RequestDTO
            {
                model = "text-davinci-003",
                prompt = $"Write a colourful biography for a fantasy character that fits these traits: {charData.race}, {charData.role}, {charData.alignmentX} {charData.alignmentY}. Start by saying the character name in upper case. Maximum 975 characters",
                temperature = 0.4F,
                max_tokens = 125
            };

            var msgJson = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(uri, msgJson);

            //var stringResponse = await response.Content.ReadAsStringAsync();
            //return stringResponse;

            var streamResponse = await response.Content.ReadAsStreamAsync();
            return streamResponse;
            //var deserializedResponse = JsonSerializer.Deserialize<ResponseDTO>(streamResponse);

            //return deserializedResponse.Answers[0].text;
        }

        //[HttpPost("GetKeywords")]
        //public async Task<Stream> GetKeywords([FromBody] CharacterBio characterBio)
        //{
        //    string uri = "https://api.openai.com/v1/completions";
        //    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);

        //    RequestDTO request = new RequestDTO
        //    {
        //        model = "text-davinci-003",
        //        prompt = $"Turn the following text into a series of keywords and traits, separated by comma: {characterBio.bio}",
        //        temperature = 0.4F,
        //        max_tokens = 30
        //    };

        //    var msgJson = new StringContent(
        //        JsonSerializer.Serialize(request),
        //        Encoding.UTF8,
        //        "application/json");

        //    var response = await httpClient.PostAsync(uri, msgJson);

        //    //var stringResponse = await response.Content.ReadAsStringAsync();
        //    //return stringResponse;

        //    var streamResponse = await response.Content.ReadAsStreamAsync();
        //    return streamResponse;
        //    //var deserializedResponse = JsonSerializer.Deserialize<ResponseDTO>(streamResponse);

        //    //return deserializedResponse.Answers[0].text;
        //}

        [HttpPost("Image")]
        public async Task<Stream> GetPortrait([FromBody] CharacterPrompt charData)
        {
            string uri = "https://api.openai.com/v1/images/generations";

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _configuration["OpenAI:UserAPI"]);

            PortraitRequestDTO request = new PortraitRequestDTO
            {
                prompt = $"Portrait of this person, fantasy RPG style, very detailed and artistic, high resolution pixel graphics: {charData.race}, {charData.role}, {charData.alignmentX} {charData.alignmentY}",
                n = 1,
                size = "256x256",
                response_format = "url"
            };

            var msgJson = new StringContent(
              JsonSerializer.Serialize(request),
              Encoding.UTF8,
              "application/json");

            var response = await httpClient.PostAsync(uri, msgJson);
            var streamResponse = await response.Content.ReadAsStreamAsync();

            return streamResponse;

            //var deserializedResponse = JsonSerializer.Deserialize<ImageResponseDTO>(streamResponse);

            //return deserializedResponse.Response[0].url;
        }

        [HttpPost("UseCustomKey")]
        //[Route("~/Communications/CustomKey")]
        public async Task<Stream> GetWithKey([FromBody] CharacterPromptWithKey charData)
        {
            string uri = "https://api.openai.com/v1/completions";
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", charData.key);

            RequestDTO request = new RequestDTO
            {
                model = "text-davinci-003",
                prompt = $"Write a colourful biography for a fantasy character that fits these traits: {charData.race}, {charData.role}, {charData.alignmentX} {charData.alignmentY}. Start by saying the character name in upper case. End it with explaining why they are on an adventure and what motivates them",
                temperature = 0.4F,
                max_tokens = 300
            };

            var msgJson = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(uri, msgJson);

            //var stringResponse = await response.Content.ReadAsStringAsync();
            //return stringResponse;

            var streamResponse = await response.Content.ReadAsStreamAsync();
            return streamResponse;
            //var deserializedResponse = JsonSerializer.Deserialize<ResponseDTO>(streamResponse);

            //return deserializedResponse.Answers[0].text;
        }
    }
}