using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class RemedioController : ControllerBase
    {
        private readonly DataContext _context;

        public RemedioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remedio>>> GetRemedios()
        {
            
            var remediosList = await _context.Remedios.ToListAsync();
            if (remediosList == null)
            {
                return NotFound();
            }

            return Ok(remediosList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Remedio>> GetRemedio(int id)
        {
            var remedio = await _context.Remedios.FindAsync(id);
            if (remedio == null)
            {
                return NotFound();
            }

            return remedio;
        }

        [HttpPost]
        public async Task<ActionResult<Remedio>> PostRemedio(Remedio remedio)
        {
            _context.Remedios.Add(remedio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRemedio), new { id = remedio.IdRemedio }, remedio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRemedio(int id, Remedio remedio)
        {
            if (id != remedio.IdRemedio)
            {
                return BadRequest();
            }

            _context.Entry(remedio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemedio(int id)
        {
            var remedio = await _context.Remedios.FindAsync(id);
            if (remedio == null)
            {
                return NotFound();
            }

            _context.Remedios.Remove(remedio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
