using Microsoft.AspNetCore.Mvc;
using NTTDATAmbevSolution.Application.Interfaces;
using NTTDATAmbevSolution.Application.DTOs;
using System.Collections.Generic;

namespace NTTDATAmbevSolution.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleDto saleDto)
        {
            if (saleDto == null || saleDto.Items == null || saleDto.Items.Count == 0)
                return BadRequest("A venda deve conter pelo menos um item.");

            var sale = _saleService.CreateSale(saleDto);
            return CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleById(int id)
        {
            var sale = _saleService.GetSaleById(id);
            if (sale == null) return NotFound();

            return Ok(sale);
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            var sales = _saleService.GetAllSales();
            return Ok(sales);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSale(int id, [FromBody] SaleDto saleDto)
        {
            if (saleDto == null || saleDto.Id != id)
                return BadRequest("Dados inválidos para atualização.");

            _saleService.UpdateSale(saleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            _saleService.DeleteSale(id);
            return NoContent();
        }
    }
}
