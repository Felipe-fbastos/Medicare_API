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
    public class GrauParentescoController : ControllerBase
    {
        private readonly DataContext _context;

        public GrauParentescoController(DataContext context)
        {
            _context = context;
        }

        #region GetGrausParentesco

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrauParentesco>>> GetGrausParentesco()
        {
            try
            {
                var graus = await _context.GrausParentesco.ToListAsync();
                if (graus == null)
                {
                    return NotFound();
                }

                return Ok(graus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region GetGrauParentesco

        [HttpGet("{id}")]
        public async Task<ActionResult<GrauParentesco>> GetGrauParentesco(int id)
        {
            try
            {
                var grauParentesco = await _context.GrausParentesco.FindAsync(id);
                if (grauParentesco == null)
                {
                    return NotFound();
                }

                return Ok(grauParentesco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region PostGrauParentesco

        [HttpPost]
        public async Task<ActionResult<GrauParentesco>> PostGrauParentesco(GrauParentesco grauParentesco)
        {
            try
            {
                _context.GrausParentesco.Add(grauParentesco);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetGrauParentesco), new { id = grauParentesco.IdGrauParentesco }, grauParentesco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region PutGrauParentesco

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrauParentesco(int id, GrauParentesco grauParentesco)
        {
            var parentescoExistente = await _context.GrausParentesco
                    .FirstOrDefaultAsync(g => g.IdGrauParentesco == id);


                if (parentescoExistente == null)
                {
                    return NotFound($"Grau de parentesco com o id {id} não encontrado.");
                }

            try
            {
                _context.Entry(grauParentesco).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, $"Concurrency error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region DeleteGrauParentesco

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrauParentesco(int id)
        {
            try
            {
                var grauParentesco = await _context.GrausParentesco.FindAsync(id);
                if (grauParentesco == null)
                {
                    return NotFound();
                }

                _context.GrausParentesco.Remove(grauParentesco);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion
    }
}
