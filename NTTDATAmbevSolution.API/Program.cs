using Microsoft.OpenApi.Models;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Application.Services;
using NTTDATAAmbev.Domain.Interfaces;
using NTTDATAAmbev.Infrastructure.Data;
using NTTDATAAmbev.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); 
});

var services = builder.Services;

services.AddControllers();

services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
services.AddSingleton<ISaleService, SaleService>();

// Swagger
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NTTDATAAmbev API", Version = "v1" });
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Aplicação NTTDATAAmbev iniciando...");

if (app.Environment.IsDevelopment())
{
    logger.LogInformation("Ambiente de desenvolvimento detectado. Swagger ativado.");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    logger.LogInformation("Ambiente de produção detectado.");
}

app.UseAuthorization();
app.MapControllers();

logger.LogInformation("Aplicação pronta para receber requisições na porta 5000.");
app.Run();
