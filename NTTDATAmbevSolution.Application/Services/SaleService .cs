// ServiceControl.Application/Services/RegistroService.cs
using Microsoft.Extensions.Logging;
using ServiceControl.Application.DTOs;
using ServiceControl.Application.Interfaces;
using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Enums;
using ServiceControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceControl.Application.Services
{
    public class RegistroService : ISaleService
    {
        private readonly IClimaService _climaService;
        private readonly IRegistroRepository _repository;
        private readonly IServiceBClient _serviceB;
        private readonly ILogger<RegistroService> _logger;

        public RegistroService(
            IClimaService climaService,
            IRegistroRepository repository,
            IServiceBClient serviceB,
            ILogger<RegistroService> logger)
        {
            _climaService = climaService;
            _repository = repository;
            _serviceB = serviceB;
            _logger = logger;
        }

        public async Task<RegistroOutputDto> ProcessarRegistroAsync(RegistroInputDto dto)
        {
            try
            {
                _logger.LogInformation("Iniciando processamento do registro para {Cidade}", dto.Cidade);

                var clima = await _climaService.ObterClimaAsync(dto.Cidade);
                var condicao = clima switch
                {
                    >= 15 and <= 30 => CondicaoClimatica.OtimasCondicoes,
                    >= 10 and < 15 => CondicaoClimatica.Agradavel,
                    _ => CondicaoClimatica.Impraticavel
                };

                var registro = new Sale
                {
                    Id = Guid.NewGuid(),
                    ServicoExecutado = dto.ServicoExecutado,
                    Data = dto.Data,
                    Responsavel = dto.Responsavel,
                    Cidade = dto.Cidade,
                    Temperatura = clima,
                    CondicaoClimatica = condicao.ToString()
                };

                await _repository.SalvarAsync(registro);
                _logger.LogInformation("Registro salvo com sucesso — ID: {Id}", registro.Id);

                await _serviceB.EnviarRegistroAsync(registro);
                _logger.LogInformation("Registro enviado para ServiceB — ID: {Id}", registro.Id);

                return new RegistroOutputDto
                {
                    Id = registro.Id,
                    ServicoExecutado = registro.ServicoExecutado,
                    Data = registro.Data,
                    Responsavel = registro.Responsavel,
                    Temperatura = registro.Temperatura,
                    CondicaoClimatica = registro.CondicaoClimatica,
                    Cidade = registro.Cidade
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao processar registro para {Cidade}", dto.Cidade);
                throw;
            }
        }

        public async Task<IEnumerable<RegistroOutputDto>> ObterTodosAsync()
        {
            try
            {
                _logger.LogInformation("Recuperando todos os registros...");
                var registros = await _repository.ObterTodosAsync();
                _logger.LogInformation("Encontrados {Count} registros", registros.Count());

                return registros.Select(r => new RegistroOutputDto
                {
                    Id = r.Id,
                    ServicoExecutado = r.ServicoExecutado,
                    Data = r.Data,
                    Responsavel = r.Responsavel,
                    Temperatura = r.Temperatura,
                    CondicaoClimatica = r.CondicaoClimatica,
                    Cidade = r.Cidade
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar registros");
                throw;
            }
        }
    }
}
