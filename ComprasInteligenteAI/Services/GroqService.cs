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

    // Envia o prompt para a API da Groq e retorna a resposta da IA.
    public async Task<string> GerarRespostaAsync(string prompt)
    {
        var request = new GroqRequest
        {
            Model = _settings.Model,
            MaxTokens = _settings.MaxTokens,
            Temperature = _settings.Temperature,
            Messages =
            [
                new GroqMessage
                {
                    Role = "user",
                    Content = prompt
                }
            ]
        };

        var json = JsonSerializer.Serialize(request);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _settings.ApiKey);

        var response = await _httpClient.PostAsync(
            "https://api.groq.com/openai/v1/chat/completions",
            content);

        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();

        var resultado = JsonSerializer.Deserialize<GroqResponse>(responseJson);

        return resultado?.Choices.FirstOrDefault()?.Message.Content
               ?? "Não foi possível gerar uma resposta.";
    }
}