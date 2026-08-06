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

        if (request.ValorEstimado <= 0)
        {
            return BadRequest("Informe um valor estimado maior que zero.");
        }

        if (string.IsNullOrWhiteSpace(request.Departamento))
        {
            return BadRequest("O departamento é obrigatório.");
        }

        try
        {
            var resposta = await _aiService.AnalisarCompraAsync(
                request.Descricao,
                request.ValorEstimado,
                request.Departamento);

            var resultado = JsonSerializer.Deserialize<CompraResponse>(
                resposta,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (resultado == null)
            {
                return StatusCode(500, new
                {
                    erro = "Não foi possível interpretar a resposta da IA.",
                    respostaRecebida = resposta
                });
            }

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                erro = ex.Message
            });
        }
    }
}