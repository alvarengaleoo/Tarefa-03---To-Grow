using System.Text.Json;
using ComprasInteligenteAI.DTOs;
using ComprasInteligenteAI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComprasInteligenteAI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private readonly AIService _aiService;

    public ComprasController(AIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("analisar")]
    public async Task<IActionResult> Analisar([FromBody] CompraRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Descricao))
        {
            return BadRequest("A descrição da compra é obrigatória.");
        }

        var resposta = await _aiService.AnalisarCompraAsync(
            request.Descricao,
            request.ValorEstimado,
            request.Departamento);

        try
        {
            var resultado = JsonSerializer.Deserialize<CompraResponse>(
                resposta,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (resultado is null)
            {
                return BadRequest("A IA retornou uma resposta inválida.");
            }

            return Ok(resultado);
        }
        catch
        {
            return BadRequest(new
            {
                erro = "Não foi possível interpretar a resposta da IA.",
                resposta
            });
        }
    }
}