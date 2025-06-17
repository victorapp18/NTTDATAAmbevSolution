// API/Controllers/SalesController.cs
using Microsoft.AspNetCore.Mvc;
using NTTDATAAmbev.Application.DTOs;
using NTTDATAAmbev.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTTDATAAmbev.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService) =>
            _saleService = saleService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SaleDto>> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            return sale is null ? NotFound() : Ok(sale);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] SaleDto saleDto)
        {
            var id = await _saleService.CreateAsync(saleDto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _saleService.CancelAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
