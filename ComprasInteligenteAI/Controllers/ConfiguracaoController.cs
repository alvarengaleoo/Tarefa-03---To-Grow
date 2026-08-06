using ComprasInteligenteAI.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ComprasInteligenteAI.Controllers;

[ApiController]
[Route("api/configuracao")]
public class ConfiguracaoController : ControllerBase
{
    private readonly AISettings _settings;

    public ConfiguracaoController(IOptions<AISettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet]
    public IActionResult Obter()
    {
        return Ok(new
        {
            modelo = _settings.Model,
            temperatura = _settings.Temperature,
            maxTokens = _settings.MaxTokens
        });
    }
}