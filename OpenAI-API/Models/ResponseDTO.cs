using System.Text.Json.Serialization;

namespace OpenAI_API.Models;

public class AnswerDTO
{
    public string text { get; set; }
    public int? index { get; set; }
    public string? finish_reason { get; set; }
}

public class ConsumptionDTO
{
    public int? prompt_tokens { get; set; }
    public int? completion_tokens {  get; set; }
    public int? total_tokens { get; set; }
}


public class ResponseDTO
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("object")]
    public string? Job { get; set; }
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("created")]
    public int? Created { get; set; }
    [JsonPropertyName("choices")]
    public AnswerDTO[]? Answers { get; set; }
    [JsonPropertyName("usage")]
    public ConsumptionDTO? Costs { get; set; }
}
