using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class HistoricoPosologiaController : ControllerBase
    {
        private readonly DataContext _context;

        public HistoricoPosologiaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoPosologia>>> GetHistoricosPosologia()
        {
            
            var historicos = await _context.HistoricosPosologia.ToListAsync();
            if (historicos == null)
            {
                return NotFound();
            }

            return Ok(historicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoPosologia>> GetHistoricoPosologia(int id)
        {
            var historicoPosologia = await _context.HistoricosPosologia.FindAsync(id);
            if (historicoPosologia == null)
            {
                return NotFound();
            }

            return Ok(historicoPosologia);
        }

        [HttpPost]
        public async Task<ActionResult<HistoricoPosologia>> PostHistoricoPosologia(HistoricoPosologia historicoPosologia)
        {
            _context.HistoricosPosologia.Add(historicoPosologia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHistoricoPosologia), new { id = historicoPosologia.IdPosologia }, historicoPosologia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoricoPosologia(int id, HistoricoPosologia historicoPosologia)
        {
            if (id != historicoPosologia.IdPosologia)
            {
                return BadRequest();
            }

            _context.Entry(historicoPosologia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoricoPosologia(int id)
        {
            var historicoPosologia = await _context.HistoricosPosologia.FindAsync(id);
            if (historicoPosologia == null)
            {
                return NotFound();
            }

            _context.HistoricosPosologia.Remove(historicoPosologia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
