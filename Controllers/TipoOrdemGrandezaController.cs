using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class TipoOrdemGrandezaController : ControllerBase
    {
        private readonly DataContext _context;

        public TipoOrdemGrandezaController(DataContext context)
        {
            _context = context;
        }

        #region GetTipoOrdemGrandezas

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoOrdemGrandeza>>> GetTipoOrdemGrandezas()
        {
            try
            {
                var tiposGrandeza = await _context.TiposOrdemGrandeza.ToListAsync();
                if (tiposGrandeza == null || !tiposGrandeza.Any())
                {
                    return NotFound("Nenhum tipo de ordem de grandeza encontrado.");
                }

                return Ok(tiposGrandeza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetTipoOrdemGrandeza

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoOrdemGrandeza>> GetTipoOrdemGrandeza(int id)
        {
            try
            {
                var tipoOrdemGrandeza = await _context.TiposOrdemGrandeza.FindAsync(id);
                if (tipoOrdemGrandeza == null)
                {
                    return NotFound($"Tipo de ordem de grandeza com id {id} não encontrado.");
                }

                return Ok(tipoOrdemGrandeza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostTipoOrdemGrandeza

        [HttpPost]
        public async Task<ActionResult<TipoOrdemGrandeza>> PostTipoOrdemGrandeza([FromBody] TipoOrdemGrandeza tipoOrdemGrandeza)
        {
            if (tipoOrdemGrandeza == null)
            {
                return BadRequest("Os dados do tipo de ordem de grandeza são obrigatórios.");
            }

            try
            {
                _context.TiposOrdemGrandeza.Add(tipoOrdemGrandeza);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoOrdemGrandeza), new { id = tipoOrdemGrandeza.IdTipoOrdemGrandeza }, tipoOrdemGrandeza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutTipoOrdemGrandeza

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoOrdemGrandeza(int id, [FromBody] TipoOrdemGrandeza tipoOrdemGrandeza)
        {
            var grandezaExistente = await _context.TiposOrdemGrandeza
                    .FirstOrDefaultAsync(g => g.IdTipoOrdemGrandeza  == id);


                if (grandezaExistente == null)
                {
                    return NotFound($"Grandeza com o id {id} não encontrada.");
                }

            if (tipoOrdemGrandeza == null)
            {
                return BadRequest("Os dados do tipo de ordem de grandeza são obrigatórios.");
            }

            try
            {
                _context.Entry(tipoOrdemGrandeza).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoOrdemGrandezaExists(id))
                {
                    return NotFound($"Tipo de ordem de grandeza com id {id} não encontrado.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeleteTipoOrdemGrandeza

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoOrdemGrandeza(int id)
        {
            try
            {
                var tipoOrdemGrandeza = await _context.TiposOrdemGrandeza.FindAsync(id);
                if (tipoOrdemGrandeza == null)
                {
                    return NotFound($"Tipo de ordem de grandeza com id {id} não encontrado.");
                }

                _context.TiposOrdemGrandeza.Remove(tipoOrdemGrandeza);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private bool TipoOrdemGrandezaExists(int id)
        {
            return _context.TiposOrdemGrandeza.Any(e => e.IdTipoOrdemGrandeza == id);
        }

        #endregion
    }
}
