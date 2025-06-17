using Microsoft.OpenApi.Models;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Application.Services;
using NTTDATAAmbev.Domain.Interfaces;
using NTTDATAAmbev.Infrastructure.Data;
using NTTDATAAmbev.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura o Kestrel para escutar na porta 5000 (HTTP)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Isso garante que a aplicação funcione no Docker
});

// Controllers
builder.Services.AddControllers();

// InMemory repository e serviço
builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
builder.Services.AddSingleton<ISaleService, SaleService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NTTDATAAmbev API", Version = "v1" });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS removido pois o container só está com HTTP (porta 5000)
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
