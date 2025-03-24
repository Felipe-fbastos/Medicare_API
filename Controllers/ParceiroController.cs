using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class ParceiroController : ControllerBase
    {
        private readonly DataContext _context;

        public ParceiroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parceiro>>> GetParceiros()
        {
            return await _context.Parceiros.ToListAsync();
            var parceirosList = await _context.Parceiros.ToListAsync();
            if (parceirosList == null)
            {
                return NotFound();
            }

            return Ok(parceirosList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Parceiro>> GetParceiro(int id)
        {
            var parceiro = await _context.Parceiros.FindAsync(id);
            if (parceiro == null)
            {
                return NotFound();
            }

            return parceiro;
        }

        [HttpPost]
        public async Task<ActionResult<Parceiro>> PostParceiro(Parceiro parceiro)
        {
            _context.Parceiros.Add(parceiro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParceiro), new { id = parceiro.IdParceiro }, parceiro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParceiro(int id, Parceiro parceiro)
        {
            if (id != parceiro.IdParceiro)
            {
                return BadRequest();
            }

            _context.Entry(parceiro).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParceiro(int id)
        {
            var parceiro = await _context.Parceiros.FindAsync(id);
            if (parceiro == null)
            {
                return NotFound();
            }

            _context.Parceiros.Remove(parceiro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}