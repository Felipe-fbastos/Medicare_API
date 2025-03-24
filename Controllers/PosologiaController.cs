using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class PosologiaController : ControllerBase
    {
        private readonly DataContext _context;

        public PosologiaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posologia>>> GetPosologias()
        {
            var posologias = await  _context.Posologias.ToListAsync();
            if (posologias == null)
            {
                return NotFound();
            }

            return Ok(posologias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posologia>> GetPosologia(int id)
        {
            var posologiaList = await _context.Posologias.FindAsync(id);
            if (posologiaList == null)
            {
                return NotFound();
            }

            return Ok(posologiaList);
        }

        [HttpPost]
        public async Task<ActionResult<Posologia>> PostPosologia(Posologia posologia)
        {
            _context.Posologias.Add(posologia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPosologia), new { id = posologia.IdPosologia }, posologia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosologia(int id, Posologia posologia)
        {
            if (id != posologia.IdPosologia)
            {
                return BadRequest();
            }

            _context.Entry(posologia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosologia(int id)
        {
            var posologia = await _context.Posologias.FindAsync(id);
            if (posologia == null)
            {
                return NotFound();
            }

            _context.Posologias.Remove(posologia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
