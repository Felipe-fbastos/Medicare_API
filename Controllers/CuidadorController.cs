using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class CuidadorController : ControllerBase
    {
        private readonly DataContext _context;

        public CuidadorController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuidador>>> GetCuidadores()
        {  
            var cuidadores = await _context.Cuidadores.ToListAsync();

            if (cuidadores == null || !cuidadores.Any())
                return NotFound();

            return Ok(cuidadores);

          
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuidador>> GetCuidador(int id)
        {
            var cuidador = await _context.Cuidadores.FindAsync(id);

            if (cuidador == null)
            {
                return NotFound();
            }

            return Ok(cuidador);
        }

        
        [HttpPost]
        public async Task<ActionResult<Cuidador>> PostCuidador(Cuidador cuidador)
        {
            _context.Cuidadores.Add(cuidador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuidador), new { id = cuidador.IdCuidador}, cuidador);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuidador(int id)
        {
            var cuidador = await _context.Cuidadores.FindAsync(id);
            if (cuidador == null)
            {
                return NotFound();
            }

            _context.Cuidadores.Remove(cuidador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}