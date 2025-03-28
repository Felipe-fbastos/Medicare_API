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
            try
            {
                var laboratoriosList = await _context.Laboratorios.ToListAsync();
                if (laboratoriosList == null || !laboratoriosList.Any())
                {
                    return NotFound();
                }

                return Ok(laboratoriosList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratorio>> GetLaboratorio(int id)
        {
            try
            {
                var laboratorio = await _context.Laboratorios.FindAsync(id);
                if (laboratorio == null)
                {
                    return NotFound();
                }

                return Ok(laboratorio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Laboratorio>> PostLaboratorio(Laboratorio laboratorio)
        {
            try
            {
                _context.Laboratorios.Add(laboratorio);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLaboratorio), new { id = laboratorio.IdLaboratorio }, laboratorio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratorio(int id, Laboratorio laboratorio)
        {
            try
            {
                var laboratorioExistente = await _context.Laboratorios
                    .FirstOrDefaultAsync(l => l.IdLaboratorio  == id);


                if (laboratorioExistente == null)
                {
                    return NotFound($"Laboratório com o id {id} não encontrado.");
                }

                _context.Entry(laboratorio).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Laboratorios.Any(l => l.IdLaboratorio == id))
                {
                    return NotFound("Laboratório não encontrado.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratorio(int id)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
