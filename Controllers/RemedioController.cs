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
    public class RemedioController : ControllerBase
    {
        private readonly DataContext _context;

        public RemedioController(DataContext context)
        {
            _context = context;
        }

        #region GetRemedios

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remedio>>> GetRemedios()
        {
            try
            {
                var remediosList = await _context.Remedios.ToListAsync();
                if (remediosList == null || remediosList.Count == 0)
                {
                    return NotFound("Nenhum remédio encontrado.");
                }

                return Ok(remediosList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetRemedio

        [HttpGet("{id}")]
        public async Task<ActionResult<Remedio>> GetRemedio(int id)
        {
            try
            {
                var remedio = await _context.Remedios.FindAsync(id);
                if (remedio == null)
                {
                    return NotFound($"Remédio com id {id} não encontrado.");
                }

                return Ok(remedio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutRemedio

        [HttpPut("{idRemedio}")]
        public async Task<ActionResult<Remedio>> PutRemedio(int idRemedio, [FromBody] Remedio remedio)
        {
            if (remedio.IdRemedio == 0 || remedio.IdTipoOrdemGrandeza == 0 || remedio.IdLaboratorio == 0)
            {
                return BadRequest("Os Ids são obrigatórios.");
            }

            try
            {
                var remedioExistente = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == idRemedio);

                if (remedioExistente == null)
                {
                    return NotFound($"Remédio com id {idRemedio} não encontrado.");
                }

                var tipoOrdemGrandeza = await _context.TiposOrdemGrandeza
                    .FirstOrDefaultAsync(t => t.IdTipoOrdemGrandeza == remedio.IdTipoOrdemGrandeza);

                var laboratorio = await _context.Laboratorios
                    .FirstOrDefaultAsync(l => l.IdLaboratorio == remedio.IdLaboratorio);

                if (tipoOrdemGrandeza == null || laboratorio == null)
                {
                    return BadRequest("Tipo de ordem de grandeza ou laboratório não encontrados.");
                }

                // Atualizar os dados do remédio
                remedioExistente.IdTipoOrdemGrandeza = remedio.IdTipoOrdemGrandeza;
                remedioExistente.IdLaboratorio = remedio.IdLaboratorio;
                remedioExistente.NomeRemedio = remedio.NomeRemedio;
                remedioExistente.Anotacao = remedio.Anotacao;
                remedioExistente.Dosagem = remedio.Dosagem;
                remedioExistente.DtRegistro = remedio.DtRegistro;
                remedioExistente.QtdAlerta = remedio.QtdAlerta;

                // Associar o Tipo de Ordem de Grandeza e o Laboratório
                remedioExistente.TipoOrdemGrandeza = tipoOrdemGrandeza;
                remedioExistente.Laboratorio = laboratorio;

                _context.Remedios.Update(remedioExistente);
                await _context.SaveChangesAsync();

                return Ok(remedioExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostRemedio

        [HttpPost]
        public async Task<ActionResult<Remedio>> PostRemedio([FromBody] RemedioDTO remedio)
        {
            if (remedio.IdTipoOrdemGrandeza == 0 || remedio.IdLaboratorio == 0)
            {
                return BadRequest("Os Ids de Tipo de Ordem de Grandeza e Laboratório são obrigatórios.");
            }

            try
            {
                var tipoOrdemGrandeza = await _context.TiposOrdemGrandeza
                    .FirstOrDefaultAsync(t => t.IdTipoOrdemGrandeza == remedio.IdTipoOrdemGrandeza);

                var laboratorio = await _context.Laboratorios
                    .FirstOrDefaultAsync(l => l.IdLaboratorio == remedio.IdLaboratorio);

                if (tipoOrdemGrandeza == null || laboratorio == null)
                {
                    return BadRequest("Tipo de ordem de grandeza ou laboratório não encontrados.");
                }

                var novoRemedio = new Remedio(
                    idTipoOrdemGrandeza  : remedio.IdTipoOrdemGrandeza,
                    idLaboratorio  :  remedio.IdLaboratorio,
                    nomeRemedio :  remedio.NomeRemedio,
                    anotacao :  remedio.Anotacao,
                    dosagem  : remedio.Dosagem,
                    dtRegistro  :  remedio.DtRegistro,
                    qtdAlerta  :  remedio.QtdAlerta
                );

                // Associar o Tipo de Ordem de Grandeza e o Laboratório
                novoRemedio.TipoOrdemGrandeza = tipoOrdemGrandeza;
                novoRemedio.Laboratorio = laboratorio;

                _context.Remedios.Add(novoRemedio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRemedio), new { id = novoRemedio.IdRemedio }, novoRemedio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeleteRemedio

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRemedio(int id)
        {
            try
            {
                var remedio = await _context.Remedios.FindAsync(id);
                if (remedio == null)
                {
                    return NotFound($"Remédio com id {id} não encontrado.");
                }

                _context.Remedios.Remove(remedio);
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
