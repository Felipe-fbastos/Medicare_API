using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class TipoOrdemGrandezaController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoOrdemGrandezaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoOrdemGrandeza>>> GetTipoOrdemGrandezas()
        {
            
            var tiposGrandeza = await _context.TipoOrdemGrandeza.ToListAsync();
            if (tiposGrandeza == null)
            {
                return NotFound();
            }

            return Ok(tiposGrandeza);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoOrdemGrandeza>> GetTipoOrdemGrandeza(int id)
        {
            var tipoOrdemGrandezaList = await _context.TipoOrdemGrandeza.FindAsync(id);
            if (tipoOrdemGrandezaList == null)
            {
                return NotFound();
            }

            return Ok(tipoOrdemGrandezaList);
        }

        [HttpPost]
        public async Task<ActionResult<TipoOrdemGrandeza>> PostTipoOrdemGrandeza(TipoOrdemGrandeza tipoOrdemGrandeza)
        {
            _context.TipoOrdemGrandeza.Add(tipoOrdemGrandeza);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipoOrdemGrandeza), new { id = tipoOrdemGrandeza.IdTipoOrdemGrandeza }, tipoOrdemGrandeza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoOrdemGrandeza(int id, TipoOrdemGrandeza tipoOrdemGrandeza)
        {
            if (id != tipoOrdemGrandeza.IdTipoOrdemGrandeza)
            {
                return BadRequest();
            }

            _context.Entry(tipoOrdemGrandeza).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoOrdemGrandeza(int id)
        {
            var tipoOrdemGrandeza = await _context.TipoOrdemGrandeza.FindAsync(id);
            if (tipoOrdemGrandeza == null)
            {
                return NotFound();
            }

            _context.TipoOrdemGrandeza.Remove(tipoOrdemGrandeza);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
