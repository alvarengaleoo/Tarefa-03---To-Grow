using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ComprasInteligenteAI.Configurations;
using ComprasInteligenteAI.DTOs;
using Microsoft.Extensions.Options;

namespace ComprasInteligenteAI.Services;

public class GroqService
{
    private readonly HttpClient _httpClient;
    private readonly AISettings _settings;

    public GroqService(
        HttpClient httpClient,
        IOptions<AISettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    // Envia o prompt para a API da Groq e devolve a resposta da IA.
    public async Task<string> GerarRespostaAsync(string prompt)
    {
        var request = new GroqRequest
        {
            Model = _settings.Model,
            MaxTokens = _settings.MaxTokens,
            Messages =
[
    new GroqMessage
    {
        Role = "system",
        Content = """
Você é um especialista em compras corporativas.
Siga rigorosamente todas as instruções fornecidas.
Nunca ignore as regras.
"""
    },
    new GroqMessage
    {
        Role = "user",
        Content = prompt
    }
]
        };

        var json = JsonSerializer.Serialize(request);

        using var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _settings.ApiKey);

        try
        {
            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                content);

            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine("========== RESPOSTA BRUTA DA GROQ ==========");
            Console.WriteLine(responseContent);
            Console.WriteLine("============================================");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Erro ao consultar a API da Groq: {responseContent}");
            }

            var resultado = JsonSerializer.Deserialize<GroqResponse>(
                responseContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            Console.WriteLine("========== TEXTO DA IA ==========");
            Console.WriteLine(resultado?.Choices.FirstOrDefault()?.Message.Content);
            Console.WriteLine("================================");

            return resultado?.Choices.FirstOrDefault()?.Message.Content
                   ?? "A IA não retornou nenhuma resposta.";
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Falha ao comunicar com a API da Groq. {ex.Message}");
        }
    }
}