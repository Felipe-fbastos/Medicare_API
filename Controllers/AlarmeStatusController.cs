using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class AlarmeStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public AlarmeStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlarmeStatus>>> GetAlarmeStatus()
        {
            var alarmeStatus = await _context.AlarmeStatus.ToListAsync();

            if (alarmeStatus == null || !alarmeStatus.Any())
                return NotFound();

            return Ok(alarmeStatus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlarmeStatus>> GetAlarmeStatus(int id)
        {
            var alarmeStatus = await _context.AlarmeStatus.FindAsync(id);
            if (alarmeStatus == null)
            {
                return NotFound();
            }

            return Ok(alarmeStatus);
        }

        [HttpPost]
        public async Task<ActionResult<AlarmeStatus>> PostAlarmeStatus(AlarmeStatus alarmeStatus)
        {
            _context.AlarmeStatus.Add(alarmeStatus);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAlarmeStatus), new { id = alarmeStatus.IdAlarmeStatus }, alarmeStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlarmeStatus(int id, AlarmeStatus alarmeStatus)
        {
            if (id != alarmeStatus.IdAlarmeStatus)
            {
                return BadRequest();
            }

            _context.Entry(alarmeStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlarmeStatus(int id)
        {
            var alarmeStatus = await _context.AlarmeStatus.FindAsync(id);
            if (alarmeStatus == null)
            {
                return NotFound();
            }

            _context.AlarmeStatus.Remove(alarmeStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
