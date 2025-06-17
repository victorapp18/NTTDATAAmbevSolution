using Microsoft.OpenApi.Models;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Application.Services;
using NTTDATAAmbev.Domain.Interfaces;
using NTTDATAAmbev.Infrastructure.Data;
using NTTDATAAmbev.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
