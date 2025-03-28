using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class ResponsavelController : ControllerBase
    {
        private readonly DataContext _context;

        public ResponsavelController(DataContext context)
        {
            _context = context;
        }

        #region GET Responsaveis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Responsavel>>> GetResponsaveis()
        {
            try
            {
                var responsaveisList = await _context.Responsaveis.ToListAsync();
                if (responsaveisList == null)
                {
                    return NotFound();
                }
                return Ok(responsaveisList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar responsáveis: {ex.Message}");
            }
        }
        #endregion

        #region GET Responsavel by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Responsavel>> GetResponsavel(int id)
        {
            try
            {
                var responsavelList = await _context.Responsaveis.FindAsync(id);

                if (responsavelList == null)
                {
                    return NotFound();
                }

                return Ok(responsavelList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar responsável: {ex.Message}");
            }
        }
        #endregion

        #region PUT Responsavel
        [HttpPut("{id1}+{id2}")]
        public async Task<ActionResult<Responsavel>> PutResponsavel(int id1, int id2, [FromBody] ResponsavelDTO responsavelDTO)
        {
            try
            {
                
                // Buscar o Responsável no banco de dados pelo Id
                var responsavel = await _context.Responsaveis
                    .FirstOrDefaultAsync(r => r.IdResponsavel == id1);

                var utilizador = await _context.Responsaveis
                    .FirstOrDefaultAsync(r => r.IdResponsavel == id2);

                if (responsavel == null || utilizador == null)
                {
                    return NotFound("Relação não encontrada.");
                }

                //  Buscar os Utilizadores e o Grau de Parentesco
                var responsavelDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == responsavelDTO.IdResponsavel);

                var utilizadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == responsavelDTO.IdUtilizador);

                var grauParentesco = await _context.GrausParentesco
                    .FirstOrDefaultAsync(u => u.IdGrauParentesco == responsavelDTO.IdGrauParentesco);

                //  Se algum Utilizador ou Grau de Parentesco não for encontrado, retornar erro
                if (responsavelDt == null || utilizadorDt == null || grauParentesco == null)
                {
                    return BadRequest("Os dados de utilizador ou grau de parentesco são inválidos.");
                }

                //  Verificar se os utilizadores possuem os tipos corretos
                if (responsavelDt.IdTipoUtilizador != 2 || utilizadorDt.IdTipoUtilizador != 1)
                {
                    return BadRequest("Os utilizadores não pertencem ao tipo correto.");
                }

                //  Atualizar os dados do Responsável
                responsavel.IdGrauParentesco = responsavelDTO.IdGrauParentesco;
                responsavel.StResponsavel = responsavelDTO.StResponsavel;
                responsavel.DuResponsavel = DateTime.Now;  // A data de atualização pode ser a data atual

                //  Atualizar os Utilizadores e o Grau de Parentesco associados
                responsavel.ResponsavelUtilizador = responsavelDt;
                responsavel.Utilizador = utilizadorDt;
                responsavel.GrauParentesco = grauParentesco;

                //  Salvar as mudanças no contexto
                _context.Responsaveis.Update(responsavel);
                await _context.SaveChangesAsync();

                //  Retornar o Responsável atualizado
                return Ok(responsavel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar responsável: {ex.Message}");
            }
        }
        #endregion

        #region POST Responsavel
        [HttpPost]
        public async Task<ActionResult<Responsavel>> PostResponsavel([FromBody] ResponsavelDTO responsavelDTO)
        {
            try
            {
                // 1. Verificar se os Ids foram enviados
                if (responsavelDTO.IdResponsavel == 0 || responsavelDTO.IdUtilizador == 0)
                {
                    return BadRequest("O Id é obrigatório.");
                }

                // 2. Buscar o Utilizador
                var responsavelDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == responsavelDTO.IdResponsavel);

                var utilizadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == responsavelDTO.IdUtilizador);

                var grauParentesco = await _context.GrausParentesco
                    .FirstOrDefaultAsync(u => u.IdGrauParentesco == responsavelDTO.IdGrauParentesco);

                // 3. Se o TipoUtilizador não for encontrado, retornar erro
                if (responsavelDt == null || utilizadorDt == null)
                {
                    return BadRequest("Os utilizadores são nulos");
                }

                if (responsavelDt != null && utilizadorDt != null && (responsavelDt.IdTipoUtilizador != 2 || utilizadorDt.IdTipoUtilizador != 1))
                {
                    return BadRequest("Os utilizadores não pertencem ao tipo correto");
                }
                else
                {
                    // 4. Criar o Cuidador e preencher os dados
                    var responsavel = new Responsavel(
                        idResponsavel: responsavelDTO.IdResponsavel,
                        idUtilizador: responsavelDTO.IdUtilizador,
                        idGrauParentesco: responsavelDTO.IdGrauParentesco,
                        dcResponsavel: DateTime.Now,
                        duResponsavel: DateTime.Now,
                        stResponsavel: responsavelDTO.StResponsavel
                    );

                    // 5. Associar os Utilizadores à Cuidadores
                    responsavel.ResponsavelUtilizador = responsavelDt;
                    responsavel.Utilizador = utilizadorDt;
                    responsavel.GrauParentesco = grauParentesco;

                    // 6. Adicionar o Cuidador ao contexto e salvar
                    _context.Responsaveis.Add(responsavel);
                    await _context.SaveChangesAsync();

                    // 7. Retornar o Cuidador criado
                    return CreatedAtAction(nameof(GetResponsavel), responsavel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar responsável: {ex.Message}");
            }
        }
        #endregion

        #region DELETE Responsavel
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsavel(int id)
        {
            try
            {
                var responsavel = await _context.Responsaveis.FindAsync(id);
                if (responsavel == null)
                {
                    return NotFound();
                }

                _context.Responsaveis.Remove(responsavel);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir responsável: {ex.Message}");
            }
        }
        #endregion
    }
}
