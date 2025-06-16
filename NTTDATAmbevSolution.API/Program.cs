using Microsoft.OpenApi.Models;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Application.Services;
using NTTDATAAmbev.Domain.Interfaces;
using NTTDATAAmbev.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register InMemory repository and service (singleton para manter dados na memória enquanto o app roda)
builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
builder.Services.AddSingleton<ISaleService, SaleService>();

// Add swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NTTDATAAmbev API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NTTDATAAmbev API v1");
        c.RoutePrefix = string.Empty; // Swagger UI na raiz
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
