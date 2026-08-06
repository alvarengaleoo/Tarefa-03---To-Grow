using ComprasInteligenteAI.Configurations;
using ComprasInteligenteAI.Prompts;
using ComprasInteligenteAI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração da IA
builder.Services.Configure<AISettings>(
    builder.Configuration.GetSection("AISettings"));

// Registro dos serviços da aplicação
builder.Services.AddScoped<PromptBuilder>();
builder.Services.AddScoped<AIService>();
builder.Services.AddHttpClient<GroqService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();