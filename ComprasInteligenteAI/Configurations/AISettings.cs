namespace ComprasInteligenteAI.Configurations;

public class AISettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public double Temperature { get; set; }

    public int MaxTokens { get; set; }
}