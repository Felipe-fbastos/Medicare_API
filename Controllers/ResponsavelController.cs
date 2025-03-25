using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class ResponsavelController : ControllerBase
    {
        private readonly DataContext _context;

        public ResponsavelController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsavel>>> GetResponsaveis()
        {
            var responsaveisList = await _context.Responsaveis.ToListAsync();
            if (responsaveisList == null)
            {
                return NotFound();
            }

            return Ok(responsaveisList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Responsavel>> GetResponsavel(int id)
        {
            var responsavelList = await _context.Responsaveis.FindAsync(id);

            if (responsavelList == null)
            {
                return NotFound();
            }

            return Ok(responsavelList);
        }

        [HttpPost]
        public async Task<ActionResult<Responsavel>> PostResponsavel(Responsavel responsavel)
        {
            _context.Responsaveis.Add(responsavel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetResponsavel), new { id = responsavel.IdResponsavel }, responsavel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsavel(int id)
        {
            var responsavel = await _context.Responsaveis.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }

            _context.Responsaveis.Remove(responsavel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}