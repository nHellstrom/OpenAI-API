namespace OpenAI_API.Models;

public class RequestDTO
{
    public string model {  get; set; }
    public string prompt { get; set; } 
    public float temperature { get; set; }
    public int max_tokens { get; set; }
}

public class PortraitRequestDTO
{
    public string prompt { set; get; }
    public int n { get; set; }
    public string size { get; set; }
    public string response_format { get; set; }
}

public class CharacterPrompt
{
    public string race { get; set;}
    public string role { get; set;}
    public string alignmentX { get; set;}
    public string alignmentY { get; set;}
}

public class CharacterBio
{
    public string bio { get; set; }
}

public class CharacterPromptWithKey
{
    public string key { get; set;}
    public string race { get; set; }
    public string role { get; set; }
    public string alignmentX { get; set; }
    public string alignmentY { get; set; }
}

