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

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET api/sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        // GET api/sales/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SaleDto>> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        // POST api/sales
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] SaleDto saleDto)
        {
            if (saleDto == null) return BadRequest();

            try
            {
                var newId = await _saleService.CreateAsync(saleDto);
                return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/sales/{id}/cancel
        [HttpPut("{id:guid}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var cancelled = await _saleService.CancelAsync(id);
            if (!cancelled) return NotFound();
            return NoContent();
        }
    }
}
