namespace ComprasInteligenteAI.Prompts;

public class PromptBuilder
{
    public string CriarPrompt(
        string descricao,
        decimal valorEstimado,
        string departamento)
    {
        return $$"""
Você é um analista de compras.

Departamento: {{departamento}}

Valor: R$ {{valorEstimado:N2}}

Descrição:
{{descricao}}

Analise a solicitação e classifique:

- Categoria (Administrativa, Operacional ou Estratégica)
- Prioridade (Baixa, Média ou Alta)
- Risco (Baixo, Médio ou Alto)

Explique a justificativa e forneça uma sugestão.

Retorne apenas um JSON válido no formato:

{
  "categoria": "",
  "prioridade": "",
  "risco": "",
  "justificativa": "",
  "sugestao": ""
}
""";
    }
}