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
    public class HistoricoPosologiaController : ControllerBase
    {
        private readonly DataContext _context;

        public HistoricoPosologiaController(DataContext context)
        {
            _context = context;
        }

        #region GetHistoricosPosologia

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoPosologia>>> GetHistoricosPosologia()
        {
            try
            {
                var historicos = await _context.HistoricosPosologia.ToListAsync();
                if (historicos == null || historicos.Count == 0)
                {
                    return NotFound("Nenhum histórico de posologia encontrado.");
                }

                return Ok(historicos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetHistoricoPosologia

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoPosologia>> GetHistoricoPosologia(int id)
        {
            try
            {
                var historicoPosologia = await _context.HistoricosPosologia.FindAsync(id);
                if (historicoPosologia == null)
                {
                    return NotFound($"Histórico de posologia com o id {id} não encontrado.");
                }

                return Ok(historicoPosologia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutHistoricoPosologia

        [HttpPut("{idPosologia}+{idRemedio}")]
        public async Task<ActionResult<HistoricoPosologia>> PutHistoricoPosologia(int idPosologia, int idRemedio, [FromBody] HistoricoPosologiaDTO historicoPosologiaDTO)
        {
            if (historicoPosologiaDTO.IdPosologia == 0 || historicoPosologiaDTO.IdRemedio == 0)
            {
                return BadRequest("Os Ids de Posologia e Remédio são obrigatórios.");
            }

            try
            {
                var historicoPosologia = await _context.HistoricosPosologia
                    .FirstOrDefaultAsync(hp => hp.IdPosologia == idPosologia && hp.IdRemedio == idRemedio);

                if (historicoPosologia == null)
                {
                    return NotFound("Histórico de Posologia não encontrado.");
                }

                var posologia = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdPosologia == historicoPosologiaDTO.IdPosologia);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == historicoPosologiaDTO.IdRemedio);

                if (posologia == null || remedio == null)
                {
                    return BadRequest("Posologia ou Remédio não encontrados.");
                }

                historicoPosologia.IdPosologia = historicoPosologiaDTO.IdPosologia;
                historicoPosologia.IdRemedio = historicoPosologiaDTO.IdRemedio;
                historicoPosologia.SdPosologia = historicoPosologiaDTO.SdPosologia;

                historicoPosologia.Posologia = posologia;
                historicoPosologia.Remedio = remedio;

                _context.HistoricosPosologia.Update(historicoPosologia);
                await _context.SaveChangesAsync();

                return Ok(historicoPosologia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostHistoricoPosologia

        [HttpPost]
        public async Task<ActionResult<HistoricoPosologia>> PostHistoricoPosologia([FromBody] HistoricoPosologiaDTO historicoPosologiaDTO)
        {
            if (historicoPosologiaDTO.IdPosologia == 0 || historicoPosologiaDTO.IdRemedio == 0)
            {
                return BadRequest("Os Ids de Posologia e Remédio são obrigatórios.");
            }

            try
            {
                var posologia = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdPosologia == historicoPosologiaDTO.IdPosologia);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == historicoPosologiaDTO.IdRemedio);

                if (posologia == null || remedio == null)
                {
                    return BadRequest("Posologia ou Remédio não encontrados.");
                }

                var historicoPosologia = new HistoricoPosologia(
                    idPosologia: historicoPosologiaDTO.IdPosologia,
                    idRemedio: historicoPosologiaDTO.IdRemedio,
                    sdPosologia: historicoPosologiaDTO.SdPosologia
                );

                historicoPosologia.Posologia = posologia;
                historicoPosologia.Remedio = remedio;

                _context.HistoricosPosologia.Add(historicoPosologia);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetHistoricoPosologia), new { idPosologia = historicoPosologia.IdPosologia, idRemedio = historicoPosologia.IdRemedio }, historicoPosologia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeleteHistoricoPosologia

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoricoPosologia(int id)
        {
            try
            {
                var historicoPosologia = await _context.HistoricosPosologia.FindAsync(id);
                if (historicoPosologia == null)
                {
                    return NotFound($"Histórico de Posologia com o id {id} não encontrado.");
                }

                _context.HistoricosPosologia.Remove(historicoPosologia);
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
