using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class CuidadorController : ControllerBase
    {
        private readonly DataContext _context;

        public CuidadorController(DataContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuidador>>> GetCuidadores()
        {
            try
            {
                var cuidadores = await _context.Cuidadores.ToListAsync();

                if (cuidadores == null || !cuidadores.Any())
                    return NotFound();

                return Ok(cuidadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuidador>> GetCuidador(int id)
        {
            try
            {
                var cuidador = await _context.Cuidadores.FindAsync(id);

                if (cuidador == null)
                {
                    return NotFound();
                }

                return Ok(cuidador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Put
        [HttpPut("{id1}+{id2}")]
        public async Task<ActionResult<Cuidador>> PutCuidador(int id1, int id2, [FromBody] CuidadorDTO cuidadorDTO)
        {
            try
            {
                

                // 1. Verificar se o IdTipoUtilizador foi enviado
                if (cuidadorDTO.IdCuidador == 0 || cuidadorDTO.IdUtilizador == 0)
                {
                    return BadRequest("O Id é obrigatório.");
                }

                // 2. Buscar o Cuidador pelo Id
                var cuidador = await _context.Cuidadores
                    .FirstOrDefaultAsync(c => c.IdCuidador == id1);
                var utilizador = await _context.Cuidadores
                    .FirstOrDefaultAsync(c => c.IdUtilizador == id2);

                if (cuidador == null || utilizador == null)
                {
                    return NotFound("Usuário(s) não encontrado.");
                }

                // 3. Buscar os Utilizadores
                var cuidadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == cuidadorDTO.IdCuidador);

                var utilizadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == cuidadorDTO.IdUtilizador);

                // 4. Se algum Utilizador não for encontrado, retornar erro
                if (cuidadorDt == null || utilizadorDt == null)
                {
                    return BadRequest("Os utilizadores são nulos.");
                }

                // 5. Verificar se os utilizadores são do tipo correto
                if (cuidadorDt.IdTipoUtilizador != 2 && utilizadorDt.IdTipoUtilizador != 1)
                {
                    return BadRequest("Os utilizadores não pertencem ao tipo correto.");
                }

                // 6. Atualizar os dados do Cuidador
                cuidador.IdCuidador = cuidadorDTO.IdCuidador;
                cuidador.IdUtilizador = cuidadorDTO.IdUtilizador;
                cuidador.DtInicio = cuidadorDTO.DtInicio;
                cuidador.DtFim = cuidadorDTO.DtFim;
                cuidador.StCuidador = cuidadorDTO.StCuidador;
                cuidador.DuCuidador = DateTime.Now;  // Pode ser alterado conforme necessidade
                cuidador.CuidadorUtilizador = cuidadorDt;
                cuidador.Utilizador = utilizadorDt;

                // 8. Salvar as mudanças no contexto
                _context.Cuidadores.Update(cuidador);
                await _context.SaveChangesAsync();

                // 9. Retornar o Cuidador atualizado
                return Ok(cuidador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<Cuidador>> PostCuidador([FromBody] CuidadorDTO cuidadorDTO)
        {
            try
            {
                // 1. Verificar se o IdTipoUtilizador foi enviado
                if (cuidadorDTO.IdCuidador == 0)
                {
                    return BadRequest("O IdTipoUtilizador é obrigatório.");
                }

                // 2. Buscar o Utilizador
                var cuidadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == cuidadorDTO.IdCuidador);

                var utilizadorDt = await _context.Utilizadores
                    .FirstOrDefaultAsync(u => u.IdUtilizador == cuidadorDTO.IdUtilizador);

                
                // 3. Se o TipoUtilizador não for encontrado, retornar erro
                if (cuidadorDt == null || utilizadorDt == null)
                {
                    return BadRequest("Os utilizadores são nulos");
                }

                if (cuidadorDt != null && utilizadorDt != null && (cuidadorDt.IdTipoUtilizador != 2 || utilizadorDt.IdTipoUtilizador != 1))
                {
                    return BadRequest("Os utilizadores não pertencem ao tipo correto");
                }
                else
                {
                    // 4. Criar o Cuidador e preencher os dados
                    var cuidador = new Cuidador(
                        idCuidador: cuidadorDTO.IdCuidador,
                        idUtilizador: cuidadorDTO.IdUtilizador,
                        dtInicio: cuidadorDTO.DtInicio,
                        dtFim: cuidadorDTO.DtFim,
                        dcCuidador: DateTime.Now,
                        duCuidador: DateTime.Now,
                        stCuidador: cuidadorDTO.StCuidador
                    );

                    // 5. Associar os Utilizadores à Cuidadores
                    cuidador.CuidadorUtilizador = cuidadorDt;
                    cuidador.Utilizador = utilizadorDt;

                    // 6. Adicionar o Cuidador ao contexto e salvar
                    _context.Cuidadores.Add(cuidador);
                    await _context.SaveChangesAsync();

                    // 7. Retornar o Cuidador criado
                    return CreatedAtAction(nameof(GetCuidador), cuidador);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuidador(int id)
        {
            try
            {
                var cuidador = await _context.Cuidadores.FindAsync(id);
                if (cuidador == null)
                {
                    return NotFound();
                }

                _context.Cuidadores.Remove(cuidador);
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
