using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{

    [Route("[controller]")]
    [ApiController]
    public class AlarmeController : ControllerBase
    {
        private readonly DataContext _context;

        public AlarmeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alarme>>> GetAlarmes()
        {
            var alarmes = await _context.Alarmes.ToListAsync();

            if (alarmes == null || !alarmes.Any())
                return NotFound();

            return Ok(alarmes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alarme>> GetAlarme(int id)
        {
            var alarme = await _context.Alarmes.FindAsync(id);
            if (alarme == null)
            {
                return NotFound();
            }

            return Ok(alarme);
        }

        [HttpPost]
        public async Task<ActionResult<Alarme>> PostAlarme(Alarme alarme)
        {
            _context.Alarmes.Add(alarme);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAlarme), new { id = alarme.IdAlarme }, alarme);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlarme(int id, Alarme alarme)
        {
            if (id != alarme.IdAlarme)
            {
                return BadRequest();
            }

            _context.Entry(alarme).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlarme(int id)
        {
            var alarme = await _context.Alarmes.FindAsync(id);
            if (alarme == null)
            {
                return NotFound();
            }

            _context.Alarmes.Remove(alarme);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
