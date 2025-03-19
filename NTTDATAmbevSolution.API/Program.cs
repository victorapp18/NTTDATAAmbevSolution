using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NTTDATAmbevSolution.Application.Interfaces;
using NTTDATAmbevSolution.Application.Services;
using NTTDATAmbevSolution.Domain.Interfaces;
using NTTDATAmbevSolution.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>(); 
builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
