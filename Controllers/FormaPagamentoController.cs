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
        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetFormaPagamentos()
        {
            try
            {
                var formas = await _context.FormasPagamento.ToListAsync();

                if (formas == null || !formas.Any())
                    return NotFound();

                return Ok(formas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPagamento>> GetFormaPagamento(int id)
        {
            try
            {
                var formaPagamento = await _context.FormasPagamento.FindAsync(id);
                if (formaPagamento == null)
                {
                    return NotFound();
                }

                return Ok(formaPagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<ActionResult<FormaPagamento>> PostFormaPagamento(FormaPagamento formaPagamento)
        {
            try
            {
                _context.FormasPagamento.Add(formaPagamento);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFormaPagamento), new { id = formaPagamento.IdFormaPagamento }, formaPagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPagamento(int id, FormaPagamento formaPagamento)
        {
            try
            {   
                var formaExistente = await _context.FormasPagamento
                    .FirstOrDefaultAsync(f => f.IdFormaPagamento == id);


                if (formaExistente == null)
                {
                    return NotFound($"Forma de pagamento com o id {id} não encontrada.");
                }

                _context.Entry(formaPagamento).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FormasPagamento.Any(e => e.IdFormaPagamento == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region  Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPagamento(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}