using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class GrauParentescoController : ControllerBase
    {
        private readonly DataContext _context;

        public GrauParentescoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrauParentesco>>> GetGrausParentesco()
        {
            var graus = await _context.GrauParentesco.ToListAsync();
            if (graus == null)
            {
                return NotFound();
            }

            return Ok(graus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrauParentesco>> GetGrauParentesco(int id)
        {
            var grauParentesco = await _context.GrauParentesco.FindAsync(id);
            if (grauParentesco == null)
            {
                return NotFound();
            }

            return Ok(grauParentesco);
        }

        [HttpPost]
        public async Task<ActionResult<GrauParentesco>> PostGrauParentesco(GrauParentesco grauParentesco)
        {
            _context.GrauParentesco.Add(grauParentesco);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGrauParentesco), new { id = grauParentesco.IdGrauParentesco }, grauParentesco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrauParentesco(int id, GrauParentesco grauParentesco)
        {
            if (id != grauParentesco.IdGrauParentesco)
            {
                return BadRequest();
            }

            _context.Entry(grauParentesco).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrauParentesco(int id)
        {
            var grauParentesco = await _context.GrauParentesco.FindAsync(id);
            if (grauParentesco == null)
            {
                return NotFound();
            }

            _context.GrauParentesco.Remove(grauParentesco);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}