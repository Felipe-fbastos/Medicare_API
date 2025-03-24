using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly DataContext _context;

        public FormaPagamentoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetFormaPagamentos()
        {
            var formas = await _context.FormasPagamento.ToListAsync();

            if (formas == null || !formas.Any())
                return NotFound();

            return Ok(formas);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPagamento>> GetFormaPagamento(int id)
        {
            var formaPagamento = await _context.FormasPagamento.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            return Ok(formaPagamento);
        }

        [HttpPost]
        public async Task<ActionResult<FormaPagamento>> PostFormaPagamento(FormaPagamento formaPagamento)
        {
            _context.FormasPagamento.Add(formaPagamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFormaPagamento), new { id = formaPagamento.IdFormaPagamento }, formaPagamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPagamento(int id, FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.IdFormaPagamento)
            {
                return BadRequest();
            }

            _context.Entry(formaPagamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPagamento(int id)
        {
            var formaPagamento = await _context.FormasPagamento.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            _context.FormasPagamento.Remove(formaPagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
