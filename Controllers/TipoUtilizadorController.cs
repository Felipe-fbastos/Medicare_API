using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class TipoUtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoUtilizadorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUtilizador>>> GetTipoUtilizadores()
        {
        
            var tipos = await _context.TipoUtilizadores.ToListAsync();
            if (tipos == null)
            {
                return NotFound();
            }

            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUtilizador>> GetTipoUtilizador(int id)
        {
            var tipoUtilizador = await _context.TipoUtilizadores.FindAsync(id);
            if (tipoUtilizador == null)
            {
                return NotFound();
            }

            return Ok(tipoUtilizador);
        }

        [HttpPost]
        public async Task<ActionResult<TipoUtilizador>> PostTipoUtilizador(TipoUtilizador tipoUtilizador)
        {
            _context.TipoUtilizadores.Add(tipoUtilizador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipoUtilizador), new { id = tipoUtilizador.IdTipoUtilizador }, tipoUtilizador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUtilizador(int id, TipoUtilizador tipoUtilizador)
        {
            if (id != tipoUtilizador.IdTipoUtilizador)
            {
                return BadRequest();
            }

            _context.Entry(tipoUtilizador).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUtilizador(int id)
        {
            var tipoUtilizador = await _context.TipoUtilizadores.FindAsync(id);
            if (tipoUtilizador == null)
            {
                return NotFound();
            }

            _context.TipoUtilizadores.Remove(tipoUtilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}