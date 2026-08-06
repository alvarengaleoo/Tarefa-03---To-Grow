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

    // Recebe a solicitação de compra e retorna a análise gerada pela IA.
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

            // Exibe exatamente o que a IA retornou.
            Console.WriteLine("========================================");
            Console.WriteLine("RESPOSTA DA IA:");
            Console.WriteLine(resposta);
            Console.WriteLine("========================================");

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
                    erro = "A IA retornou uma resposta, mas não foi possível convertê-la para o formato esperado.",
                    respostaIA = resposta
                });
            }

            return Ok(resultado);
        }
        catch (JsonException ex)
        {
            return StatusCode(500, new
            {
                erro = "Erro ao interpretar o JSON retornado pela IA.",
                detalhe = ex.Message
            });
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