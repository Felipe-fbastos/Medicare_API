using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly DataContext _context;

        public LaboratorioController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratorio>>> GetLaboratorios()
        {
            var laboratoriosList = await _context.Laboratorios.ToListAsync();
            if (laboratoriosList == null)
            {
                return NotFound();
            }

            return Ok(laboratoriosList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratorio>> GetLaboratorio(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            return Ok(laboratorio);
        }

        [HttpPost]
        public async Task<ActionResult<Laboratorio>> PostLaboratorio(Laboratorio laboratorio)
        {
            _context.Laboratorios.Add(laboratorio);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLaboratorio), new { id = laboratorio.IdLaboratorio }, laboratorio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratorio(int id, Laboratorio laboratorio)
        {
            if (id != laboratorio.IdLaboratorio)
            {
                return BadRequest();
            }

            _context.Entry(laboratorio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratorio(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
            {
                return NotFound();
            }

            _context.Laboratorios.Remove(laboratorio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
