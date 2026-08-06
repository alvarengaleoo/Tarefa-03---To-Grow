namespace ComprasInteligenteAI.Configurations;

public class AISettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    // Controla o tamanho máximo da resposta da IA.
    public int MaxTokens { get; set; }

    // Controla o nível de criatividade da resposta.
    public double Temperature { get; set; }
}