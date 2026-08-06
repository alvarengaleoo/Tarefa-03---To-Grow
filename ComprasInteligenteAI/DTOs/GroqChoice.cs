using System.Text.Json.Serialization;

namespace ComprasInteligenteAI.DTOs;

public class GroqChoice
{
    [JsonPropertyName("message")]
    public GroqMessage Message { get; set; } = new();
}