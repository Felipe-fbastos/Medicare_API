using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class ParceiroUtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public ParceiroUtilizadorController(DataContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParceiroUtilizador>>> GetParceiroUtilizadores()
        {
            return await _context.ParceiroUtilizadores.Include(pu => pu.colaborador).Include(pu => pu.Parceiro).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParceiroUtilizador>> GetParceiroUtilizador(int id)
        {
            var parceiroUtilizador = await _context.ParceiroUtilizadores
                                                    .Include(pu => pu.colaborador)
                                                    .Include(pu => pu.Parceiro)
                                                    .FirstOrDefaultAsync(pu => pu.IdParceiro == id);

            if (parceiroUtilizador == null)
            {
                return NotFound();
            }

            return parceiroUtilizador;
        }

        [HttpPost]
        public async Task<ActionResult<ParceiroUtilizador>> PostParceiroUtilizador(ParceiroUtilizador parceiroUtilizador)
        {
            _context.ParceiroUtilizadores.Add(parceiroUtilizador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParceiroUtilizador), new { id = parceiroUtilizador.IdParceiro }, parceiroUtilizador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParceiroUtilizador(int id, ParceiroUtilizador parceiroUtilizador)
        {
            if (id != parceiroUtilizador.IdParceiro)
            {
                return BadRequest();
            }

            _context.Entry(parceiroUtilizador).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParceiroUtilizador(int id)
        {
            var parceiroUtilizador = await _context.ParceiroUtilizadores.FindAsync(id);
            if (parceiroUtilizador == null)
            {
                return NotFound();
            }

            _context.ParceiroUtilizadores.Remove(parceiroUtilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}