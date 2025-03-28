using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #region GetParceiros

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parceiro>>> GetParceiros()
        {
            try
            {
                var parceirosList = await _context.Parceiros.ToListAsync();
                if (parceirosList == null || parceirosList.Count == 0)
                {
                    return NotFound("Nenhum parceiro encontrado.");
                }

                return Ok(parceirosList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetParceiro

        [HttpGet("{id}")]
        public async Task<ActionResult<Parceiro>> GetParceiro(int id)
        {
            try
            {
                var parceiro = await _context.Parceiros.FindAsync(id);
                if (parceiro == null)
                {
                    return NotFound($"Parceiro com o id {id} não encontrado.");
                }

                return Ok(parceiro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostParceiro

        [HttpPost]
        public async Task<ActionResult<Parceiro>> PostParceiro([FromBody] Parceiro parceiro)
        {
            if (parceiro == null)
            {
                return BadRequest("Os dados do parceiro são obrigatórios.");
            }

            try
            {
                _context.Parceiros.Add(parceiro);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetParceiro), new { id = parceiro.IdParceiro }, parceiro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutParceiro

        [HttpPut("{id}")]
        public async Task<IActionResult> PutParceiro(int id, [FromBody] Parceiro parceiro)
        {
            var parceiroExistente = await _context.Parceiros
                    .FirstOrDefaultAsync(p => p.IdParceiro == id);


                if (parceiroExistente == null)
                {
                    return NotFound($"Parceiro com o id {id} não encontrado.");
                }

            try
            {
                _context.Entry(parceiro).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"Erro de concorrência no servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeleteParceiro

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParceiro(int id)
        {
            try
            {
                var parceiro = await _context.Parceiros.FindAsync(id);
                if (parceiro == null)
                {
                    return NotFound($"Parceiro com o id {id} não encontrado.");
                }

                _context.Parceiros.Remove(parceiro);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion
    }
}
