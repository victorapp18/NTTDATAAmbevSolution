using ServiceControl.Domain.Enums;
using System;

namespace ServiceControl.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } 
        public string ServicoExecutado { get; set; }
        public DateTime Data { get; set; }
        public string Responsavel { get; set; }
        public string Cidade { get; set; }
        public double Temperatura { get; set; }
        public string CondicaoClimatica { get; set; }
    }
}
