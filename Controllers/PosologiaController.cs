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
    public class PosologiaController : ControllerBase
    {
        private readonly DataContext _context;

        public PosologiaController(DataContext context)
        {
            _context = context;
        }

        #region GetPosologias

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posologia>>> GetPosologias()
        {
            try
            {
                var posologias = await _context.Posologias.ToListAsync();
                if (posologias == null || posologias.Count == 0)
                {
                    return NotFound("Nenhuma posologia encontrada.");
                }

                return Ok(posologias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetPosologia

        [HttpGet("{id}")]
        public async Task<ActionResult<Posologia>> GetPosologia(int id)
        {
            try
            {
                var posologia = await _context.Posologias.FindAsync(id);
                if (posologia == null)
                {
                    return NotFound($"Posologia com o id {id} não encontrada.");
                }

                return Ok(posologia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutPosologia

        [HttpPut("{idPosologia}+{idRemedio}")]
        public async Task<ActionResult<Posologia>> PutPosologia(int idPosologia, int idRemedio, [FromBody] Posologia posologia)
        {
            if ( posologia.IdRemedio == 0 || posologia.IdUtilizador == 0)
            {
                return BadRequest("Os Ids de Posologia, Remédio e Utilizador são obrigatórios.");
            }

            try
            {
                var posologiaExistente = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdPosologia == idPosologia);

                var remedioExistente = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdRemedio == idRemedio);

                if (posologiaExistente == null || remedioExistente == null)
                {
                    return NotFound($"Posologia com o id {idPosologia} não encontrada.");
                }

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == posologia.IdRemedio);

                var utilizador = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == posologia.IdUtilizador);

                if (remedio == null || utilizador == null)
                {
                    return BadRequest("Remédio ou Utilizador não encontrados.");
                }

                posologiaExistente.IdRemedio = posologia.IdRemedio;
                posologiaExistente.IdUtilizador = posologia.IdUtilizador;
                posologiaExistente.DtInicio = posologia.DtInicio;
                posologiaExistente.DtFim = posologia.DtFim;
                posologiaExistente.Intervalo = posologia.Intervalo;
                posologiaExistente.QtdRemedio = posologia.QtdRemedio;

                posologiaExistente.Remedio = remedio;
                posologiaExistente.Utilizador = utilizador;

                _context.Posologias.Update(posologiaExistente);
                await _context.SaveChangesAsync();

                return Ok(posologiaExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostPosologia

        [HttpPost]
        public async Task<ActionResult<Posologia>> PostPosologia([FromBody] Posologia posologia)
        {
            if (posologia.IdRemedio == 0 || posologia.IdUtilizador == 0)
            {
                return BadRequest("Os Ids de Remédio e Utilizador são obrigatórios.");
            }

            try
            {
                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == posologia.IdRemedio);

                var utilizador = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == posologia.IdUtilizador);

                if (remedio == null || utilizador == null)
                {
                    return BadRequest("Remédio ou Utilizador não encontrados.");
                }

                var novaPosologia = new Posologia(
                    idPosologia : posologia.IdPosologia,
                    idRemedio : posologia.IdRemedio,
                    idUtilizador : posologia.IdUtilizador,
                    dtInicio : posologia.DtInicio,
                    dtFim : posologia.DtFim,
                    intervalo : posologia.Intervalo,
                    qtdRemedio : posologia.QtdRemedio
                );

                novaPosologia.Remedio = remedio;
                novaPosologia.Utilizador = utilizador;

                _context.Posologias.Add(novaPosologia);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPosologia), new { id = novaPosologia.IdPosologia }, novaPosologia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeletePosologia

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosologia(int id)
        {
            try
            {
                var posologia = await _context.Posologias.FindAsync(id);
                if (posologia == null)
                {
                    return NotFound($"Posologia com o id {id} não encontrada.");
                }

                _context.Posologias.Remove(posologia);
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
