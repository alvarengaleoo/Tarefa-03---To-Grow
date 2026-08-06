using ComprasInteligenteAI.Prompts;

namespace ComprasInteligenteAI.Services;

public class AIService
{
    private readonly PromptBuilder _promptBuilder;
    private readonly GroqService _groqService;

    public AIService(
        PromptBuilder promptBuilder,
        GroqService groqService)
    {
        _promptBuilder = promptBuilder;
        _groqService = groqService;
    }

    // Centraliza o fluxo de análise da solicitação.
    public async Task<string> AnalisarCompraAsync(
        string descricao,
        decimal valorEstimado,
        string departamento)
    {
        var prompt = _promptBuilder.CriarPrompt(
            descricao,
            valorEstimado,
            departamento);

        var resposta = await _groqService.GerarRespostaAsync(prompt);

        // Remove marcações Markdown caso a IA retorne o JSON dentro de um bloco de código.
        resposta = resposta
            .Replace("```json", "")
            .Replace("```JSON", "")
            .Replace("```", "")
            .Trim();

        return resposta;
    }
}