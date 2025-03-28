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
    public class PromocaoController : ControllerBase
    {
        private readonly DataContext _context;

        public PromocaoController(DataContext context)
        {
            _context = context;
        }

        #region GetPromocoes

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocao>>> GetPromocoes()
        {
            try
            {
                var promocoes = await _context.Promocoes.ToListAsync();
                if (promocoes == null || promocoes.Count == 0)
                {
                    return NotFound("Nenhuma promoção encontrada.");
                }

                return Ok(promocoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region GetPromocao

        [HttpGet("{id}")]
        public async Task<ActionResult<Promocao>> GetPromocao(int id)
        {
            try
            {
                var promocao = await _context.Promocoes.FindAsync(id);
                if (promocao == null)
                {
                    return NotFound($"Promoção com o id {id} não encontrada.");
                }

                return Ok(promocao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PutPromocao

        [HttpPut("{idPromocao}")]
        public async Task<ActionResult<Promocao>> PutPromocao(int idPromocao, [FromBody] Promocao promocao)
        {
            if ( promocao.IdFormaPagamento == 0 || promocao.IdColaborador == 0 || promocao.IdRemedio == 0)
            {
                return BadRequest("Os Ids de Promoção, Forma de pagamento, Colaborador e Remédio são obrigatórios.");
            }

            try
            {
                var promocaoExistente = await _context.Promocoes
                    .FirstOrDefaultAsync(p => p.IdPromocao == idPromocao);

                if (promocaoExistente == null)
                {
                    return NotFound($"Promoção com o id {idPromocao} não encontrada.");
                }

                var formaPagamento = await _context.FormasPagamento
                    .FirstOrDefaultAsync(f => f.IdFormaPagamento == promocao.IdFormaPagamento);

                var colaborador = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == promocao.IdColaborador);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == promocao.IdRemedio);

                if (formaPagamento == null || colaborador == null || remedio == null)
                {
                    return BadRequest("Forma de pagamento, colaborador ou remédio não encontrados.");
                }

                promocaoExistente.IdFormaPagamento = promocao.IdFormaPagamento;
                promocaoExistente.IdColaborador = promocao.IdColaborador;
                promocaoExistente.IdRemedio = promocao.IdRemedio;
                promocaoExistente.Descricao = promocao.Descricao;
                promocaoExistente.DtInicio = promocao.DtInicio;
                promocaoExistente.DtFim = promocao.DtFim;
                promocaoExistente.Valor = promocao.Valor;

                promocaoExistente.FormaPagamento = formaPagamento;
                promocaoExistente.Colaborador = colaborador;
                promocaoExistente.Remedio = remedio;

                _context.Promocoes.Update(promocaoExistente);
                await _context.SaveChangesAsync();

                return Ok(promocaoExistente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region PostPromocao

        [HttpPost]
        public async Task<ActionResult<Promocao>> PostPromocao([FromBody] Promocao promocao)
        {
            if (promocao.IdFormaPagamento == 0 || promocao.IdColaborador == 0 || promocao.IdRemedio == 0)
            {
                return BadRequest("Os Ids de Forma de pagamento, Colaborador e Remédio são obrigatórios.");
            }

            try
            {
                var formaPagamento = await _context.FormasPagamento
                    .FirstOrDefaultAsync(f => f.IdFormaPagamento == promocao.IdFormaPagamento);

                var colaborador = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == promocao.IdColaborador);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == promocao.IdRemedio);

                if (formaPagamento == null || colaborador == null || remedio == null)
                {
                    return BadRequest("Forma de pagamento, colaborador ou remédio não encontrados.");
                }

                var novaPromocao = new Promocao(
                    idFormaPagamento : promocao.IdFormaPagamento,
                    idColaborador : promocao.IdColaborador,
                    idRemedio : promocao.IdRemedio,
                    descricao : promocao.Descricao,
                    dtInicio  :  promocao.DtInicio,
                    dtFim  : promocao.DtFim,
                    valor  :  promocao.Valor
                );

                novaPromocao.FormaPagamento = formaPagamento;
                novaPromocao.Colaborador = colaborador;
                novaPromocao.Remedio = remedio;

                _context.Promocoes.Add(novaPromocao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPromocao), new { id = novaPromocao.IdPromocao }, novaPromocao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        #endregion

        #region DeletePromocao

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromocao(int id)
        {
            try
            {
                var promocao = await _context.Promocoes.FindAsync(id);
                if (promocao == null)
                {
                    return NotFound($"Promoção com o id {id} não encontrada.");
                }

                _context.Promocoes.Remove(promocao);
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
