using ComprasInteligenteAI.Configurations;
using ComprasInteligenteAI.Prompts;
using ComprasInteligenteAI.Services;

var builder = WebApplication.CreateBuilder(args);

// Carrega as configurações da IA definidas no appsettings.json.
builder.Services.Configure<AISettings>(
    builder.Configuration.GetSection("AISettings"));

// Registro dos serviços utilizados pela aplicação.
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

// Permite servir o frontend (index.html, css e js).
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();