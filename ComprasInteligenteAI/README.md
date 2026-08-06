# AI Compras Inteligente

Projeto desenvolvido como parte da **Tarefa 03 - Start To Grow**.


## Objetivo

Desenvolver uma funcionalidade em ASP.NET Core Web API integrada a um modelo de Inteligência Artificial para auxiliar o setor de compras na análise de solicitações.

A aplicação recebe as informações da compra, envia os dados para a API da Groq e retorna uma análise contendo categoria, prioridade, risco, justificativa e sugestão.

## Tecnologias utilizadas

- ASP.NET Core Web API
- C#
- HTML
- CSS
- JavaScript
- Bootstrap
- API Groq

## Funcionalidades

- Recebimento dos dados da solicitação.
- Construção do prompt para a IA.
- Integração com a API da Groq.
- Controle das configurações da IA pelo appsettings.json.
- Tratamento da resposta retornada pela IA.
- Exibição da análise na interface.

## Boas práticas utilizadas

- Separação em camadas (Controllers, Services, DTOs e Prompts).
- Configuração da IA centralizada.
- Definição de temperatura e limite de tokens.
- Tratamento de erros da API.
- Prompt estruturado para retornar somente JSON.

## Como executar

1. Clonar o repositório.
2. Configurar a chave da API da Groq no `appsettings.json`.
3. Executar o projeto.
4. Abrir o endereço informado pelo Visual Studio.

## Fluxo da aplicação

1. O usuário preenche a solicitação de compra.
2. A aplicação monta o prompt.
3. A solicitação é enviada para a API da Groq.
4. A IA realiza a análise.
5. O resultado é exibido na tela.

## Observações

O projeto foi desenvolvido para demonstrar a integração entre uma aplicação .NET e um modelo de Inteligência Artificial, aplicando conceitos de construção de prompts, controle de contexto, configuração de temperatura e limite de tokens, conforme solicitado na atividade.