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

    // Prepara o prompt e solicita a análise da IA.
    public async Task<string> AnalisarCompraAsync(
        string descricao,
        decimal valorEstimado,
        string departamento)
    {
        var prompt = _promptBuilder.CriarPrompt(
            descricao,
            valorEstimado,
            departamento);

        return await _groqService.GerarRespostaAsync(prompt);
    }
}