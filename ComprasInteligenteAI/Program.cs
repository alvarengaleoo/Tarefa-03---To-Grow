using ComprasInteligenteAI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configuração da IA
builder.Services.Configure<AISettings>(
    builder.Configuration.GetSection("AISettings"));

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