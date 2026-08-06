namespace ComprasInteligenteAI.Prompts;

public class PromptBuilder
{
    // Monta o prompt enviado para a IA.
    public string CriarPrompt(
        string descricao,
        decimal valorEstimado,
        string departamento)
    {
        return $$$"""
Você é um analista especializado no setor de compras corporativas.

Seu objetivo é analisar solicitações de compra de forma objetiva, considerando boas práticas de gestão de custos e necessidades do negócio.

Com base nas informações fornecidas, classifique a solicitação.

Dados da solicitação:

Departamento: {{departamento}}

Valor estimado: R$ {{valorEstimado:N2}}

Descrição:
{{descricao}}

Responda APENAS em formato JSON.

{
  "categoria": "",
  "prioridade": "",
  "risco": "",
  "justificativa": "",
  "sugestao": ""
}

Regras:

- categoria: informe o tipo da compra.
- prioridade: Baixa, Média ou Alta.
- risco: Baixo, Médio ou Alto.
- justificativa: explique a classificação.
- sugestao: recomende a melhor ação para o comprador.

Não escreva nenhum texto fora do JSON.
""";
    }
}