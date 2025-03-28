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
    public class ParceiroUtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public ParceiroUtilizadorController(DataContext context)
        {
            _context = context;
        }

        #region GetParceiroUtilizadores

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParceiroUtilizador>>> GetParceiroUtilizadores()
        {
            try
            {
                var parceiroUtilizadores = await _context.ParceirosUtilizador
                    .Include(pu => pu.Colaborador)
                    .Include(pu => pu.Parceiro)
                    .ToListAsync();

                if (parceiroUtilizadores == null || parceiroUtilizadores.Count == 0)
                {
                    return NotFound("Nenhum parceiro-utilizador encontrado.");
                }

                return Ok(parceiroUtilizadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetParceiroUtilizador

        [HttpGet("{id}")]
        public async Task<ActionResult<ParceiroUtilizador>> GetParceiroUtilizador(int id)
        {
            try
            {
                var parceiroUtilizador = await _context.ParceirosUtilizador
                    .Include(pu => pu.Colaborador)
                    .Include(pu => pu.Parceiro)
                    .FirstOrDefaultAsync(pu => pu.IdParceiro == id);

                if (parceiroUtilizador == null)
                {
                    return NotFound($"Parceiro-Utilizador com o id {id} não encontrado.");
                }

                return Ok(parceiroUtilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostParceiroUtilizador

        [HttpPost]
        public async Task<ActionResult<ParceiroUtilizador>> PostParceiroUtilizador([FromBody] ParceiroUtilizador parceiroUtilizador)
        {
            if (parceiroUtilizador.IdParceiro == 0 || parceiroUtilizador.IdColaborador == 0)
            {
                return BadRequest("Os Ids de Parceiro e Colaborador são obrigatórios.");
            }

            try
            {
                // Buscar Parceiro e Colaborador
                var parceiro = await _context.Parceiros.FirstOrDefaultAsync(p => p.IdParceiro == parceiroUtilizador.IdParceiro);
                var colaborador = await _context.Utilizadores.FirstOrDefaultAsync(u => u.IdUtilizador == parceiroUtilizador.IdColaborador);

                if (parceiro == null || colaborador == null)
                {
                    return NotFound("Parceiro ou Colaborador não encontrado.");
                }

                // Criar e salvar o novo ParceiroUtilizador
                var novoParceiroUtilizador = new ParceiroUtilizador(
                    idParceiro: parceiroUtilizador.IdParceiro,
                    idColaborador: parceiroUtilizador.IdColaborador
                );

                _context.ParceirosUtilizador.Add(novoParceiroUtilizador);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetParceiroUtilizador), new { id = novoParceiroUtilizador.IdParceiro }, novoParceiroUtilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutParceiroUtilizador

        [HttpPut("{idParceiro}+{idColaborador}")]
        public async Task<ActionResult<ParceiroUtilizador>> PutParceiroUtilizador(int idParceiro, int idColaborador, [FromBody] ParceiroUtilizador parceiroUtilizador)
        {
            var parceiroExistente = await _context.Parceiros
                    .FirstOrDefaultAsync(p => p.IdParceiro == idParceiro);

            var colaboradorExistente = await _context.Utilizadores
                                .FirstOrDefaultAsync(u => u.IdUtilizador == idColaborador);

            if (parceiroExistente == null  || colaboradorExistente == null)
            {
                return NotFound($"Relação Parceiro com o id {idParceiro} e colaborador {idColaborador} não encontrada.");
            }

            try
            {
                // Buscar Parceiro e Colaborador
                var parceiro = await _context.Parceiros.FirstOrDefaultAsync(p => p.IdParceiro == idParceiro);
                var colaborador = await _context.Utilizadores.FirstOrDefaultAsync(u => u.IdUtilizador == idColaborador);

                if (parceiro == null || colaborador == null)
                {
                    return NotFound("Parceiro ou Colaborador não encontrado.");
                }

                // Atualizar o ParceiroUtilizador
                parceiroUtilizador.Parceiro = parceiro;
                parceiroUtilizador.Colaborador = colaborador;

                _context.ParceirosUtilizador.Update(parceiroUtilizador);
                await _context.SaveChangesAsync();

                return Ok(parceiroUtilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeleteParceiroUtilizador

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParceiroUtilizador(int id)
        {
            try
            {
                var parceiroUtilizador = await _context.ParceirosUtilizador.FindAsync(id);

                if (parceiroUtilizador == null)
                {
                    return NotFound($"ParceiroUtilizador com o id {id} não encontrado.");
                }

                _context.ParceirosUtilizador.Remove(parceiroUtilizador);
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
