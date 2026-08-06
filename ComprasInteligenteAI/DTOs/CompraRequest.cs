namespace ComprasInteligenteAI.DTOs;

public class CompraRequest
{
    public string Descricao { get; set; } = string.Empty;

    public decimal ValorEstimado { get; set; }

    public string Departamento { get; set; } = string.Empty;
}