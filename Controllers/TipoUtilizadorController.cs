using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class TipoUtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoUtilizadorController(DataContext context)
        {
            _context = context;
        }

        #region GET TipoUtilizadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoUtilizador>>> GetTipoUtilizadores()
        {
            try
            {
                var tipos = await _context.TiposUtilizador.ToListAsync();
                if (tipos == null || tipos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(tipos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar tipos de utilizadores: {ex.Message}");
            }
        }
        #endregion

        #region GET TipoUtilizador by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUtilizador>> GetTipoUtilizador(int id)
        {
            try
            {
                var tipoUtilizador = await _context.TiposUtilizador.FindAsync(id);
                if (tipoUtilizador == null)
                {
                    return NotFound();
                }

                return Ok(tipoUtilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar tipo de utilizador: {ex.Message}");
            }
        }
        #endregion

        #region POST TipoUtilizador
        [HttpPost]
        public async Task<ActionResult<TipoUtilizador>> PostTipoUtilizador(TipoUtilizador tipoUtilizador)
        {
            try
            {
                _context.TiposUtilizador.Add(tipoUtilizador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoUtilizador), new { id = tipoUtilizador.IdTipoUtilizador }, tipoUtilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar tipo de utilizador: {ex.Message}");
            }
        }
        #endregion

        #region PUT TipoUtilizador
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUtilizador(int id, TipoUtilizador tipoUtilizador)
        {
            try
            {
                if (id != tipoUtilizador.IdTipoUtilizador)
                {
                    return BadRequest("ID do tipo de utilizador não corresponde.");
                }

                _context.Entry(tipoUtilizador).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUtilizadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Erro ao atualizar tipo de utilizador.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar tipo de utilizador: {ex.Message}");
            }
        }
        #endregion

        #region DELETE TipoUtilizador
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUtilizador(int id)
        {
            try
            {
                var tipoUtilizador = await _context.TiposUtilizador.FindAsync(id);
                if (tipoUtilizador == null)
                {
                    return NotFound();
                }

                _context.TiposUtilizador.Remove(tipoUtilizador);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir tipo de utilizador: {ex.Message}");
            }
        }
        #endregion

        #region Métodos auxiliares
        private bool TipoUtilizadorExists(int id)
        {
            return _context.TiposUtilizador.Any(e => e.IdTipoUtilizador == id);
        }
        #endregion
    }
}
