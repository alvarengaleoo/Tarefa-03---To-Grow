using System.Text.Json.Serialization;

namespace ComprasInteligenteAI.DTOs;

public class GroqResponse
{
    [JsonPropertyName("choices")]
    public List<GroqChoice> Choices { get; set; } = new();
}