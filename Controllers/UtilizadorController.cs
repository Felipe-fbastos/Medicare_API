using Medicare_API.Data;
using Medicare_API.Models;
using Medicare_API.Models.DTOs;
using Medicare_API.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public UtilizadorController(DataContext context)
        {
            _context = context;
        }

        #region GET Utilizadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizador>>> GetUtilizadores()
        {
            try
            {
                var utilizadores = await _context.Utilizadores.ToListAsync();
                if (utilizadores == null || utilizadores.Count == 0)
                {
                    return NotFound();
                }

                return Ok(utilizadores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar utilizadores: {ex.Message}");
            }
        }
        #endregion

        #region GET Utilizador by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizador>> GetUtilizador(int id)
        {
            try
            {
                var utilizador = await _context.Utilizadores.FindAsync(id);
                if (utilizador == null)
                {
                    return NotFound();
                }

                return Ok(utilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar utilizador: {ex.Message}");
            }
        }
        #endregion

        #region PUT Utilizador
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUtilizador(int id, [FromBody] UtilizadorUpdateDTO utilizadorDTO)
        {
            try
            {
                // Verificar se o IdTipoUtilizador foi enviado
                if (utilizadorDTO.IdTipoUtilizador == 0)
                {
                    return BadRequest("O IdTipoUtilizador é obrigatório.");
                }

                // Buscar o Utilizador pelo Id
                var utilizador = await _context.Utilizadores
                    .Include(u => u.TipoUtilizador) // Inclui o TipoUtilizador para garantir que ele seja carregado
                    .FirstOrDefaultAsync(u => u.IdUtilizador == id);

                // Se o Utilizador não for encontrado, retornar erro
                if (utilizador == null)
                {
                    return NotFound($"Utilizador com id {id} não encontrado.");
                }

                // Verificar se o TipoUtilizador existe no banco
                var tipoUtilizador = await _context.TiposUtilizador
                    .FirstOrDefaultAsync(t => t.IdTipoUtilizador == utilizadorDTO.IdTipoUtilizador);

                if (tipoUtilizador == null)
                {
                    return BadRequest("Tipo de Utilizador não encontrado.");
                }

                // Atualizar os campos do Utilizador com os valores do DTO
                utilizador.IdTipoUtilizador = utilizadorDTO.IdTipoUtilizador;
                utilizador.TipoUtilizador = tipoUtilizador; // Atualiza a navegação
                utilizador.CPF = utilizadorDTO.CPF;
                utilizador.Nome = utilizadorDTO.Nome;
                utilizador.Sobrenome = utilizadorDTO.Sobrenome;
                utilizador.DtNascimento = utilizadorDTO.DtNascimento;
                utilizador.Email = utilizadorDTO.Email;
                utilizador.Telefone = utilizadorDTO.Telefone;

                // Marcar o contexto para salvar as alterações
                _context.Utilizadores.Update(utilizador);

                // Salvar as mudanças no banco de dados
                await _context.SaveChangesAsync();

                // Retornar a resposta com o utilizador atualizado
                return Ok(utilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar utilizador: {ex.Message}");
            }
        }
        #endregion

        #region POST Utilizador
        [HttpPost]
        public async Task<IActionResult> CriarUtilizador([FromBody] UtilizadorCreateDTO utilizadorDTO)
        {
            try
            {
                // Buscar o TipoUtilizador com base no IdTipoUtilizador
                var tipoUtilizador = await _context.TiposUtilizador
                    .FirstOrDefaultAsync(t => t.IdTipoUtilizador == utilizadorDTO.IdTipoUtilizador);

                // Se o TipoUtilizador não for encontrado, retornar erro
                if (tipoUtilizador == null)
                {
                    return BadRequest("O IdTipoUtilizador informado não existe.");
                }

                // Criar o Utilizador e preencher os dados
                /* var utilizador = new Utilizador(
                     idTipoUtilizador: utilizadorDTO.IdTipoUtilizador,
                     cpf: utilizadorDTO.CPF,
                     nome: utilizadorDTO.Nome,
                     sobrenome: utilizadorDTO.Sobrenome,
                     dtNascimento: utilizadorDTO.DtNascimento,
                     email: utilizadorDTO.Email,
                     telefone: utilizadorDTO.Telefone
                 );*/

                

                var utilizador = new Utilizador();

                utilizador.IdTipoUtilizador = utilizadorDTO.IdTipoUtilizador;
                utilizador.Nome = utilizadorDTO.Nome;
                utilizador.Sobrenome = utilizadorDTO.Sobrenome;
                utilizador.CPF = utilizadorDTO.CPF;
                utilizador.DtNascimento = utilizadorDTO.DtNascimento;
                utilizador.Email = utilizadorDTO.Email;
                utilizador.Telefone = utilizadorDTO.Telefone;
                utilizador.SenhaString = utilizadorDTO.SenhaString;
                // Associar o TipoUtilizador ao Utilizador
                utilizador.TipoUtilizador = tipoUtilizador; // Aqui o EF preenche a navegação com o TipoUtilizador carregado

                Criptografia.CriarPasswordHash(utilizador.SenhaString, out byte[] hash, out byte[] salt);
                utilizador.SenhaString = string.Empty;
                utilizador.SenhaHash = hash;
                utilizador.SenhaSalt = salt;
                // Adicionar o Utilizador ao contexto e salvar
                _context.Utilizadores.Add(utilizador);
                await _context.SaveChangesAsync();

                // Retornar o Utilizador criado
                return CreatedAtAction(nameof(GetUtilizador), new { id = utilizador.IdUtilizador }, utilizador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar utilizador: {ex.Message}");
            }
        }
        #endregion

        private async Task<bool> UsuarioExistente(string Nome)
        {

            if (await _context.Utilizadores.AnyAsync(x => x.Nome.ToLower() == Nome.ToLower()))
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(UtilizadorAutenticarDTO credenciais)

        {
            try
            {
                

                Utilizador? utilizador = await _context.Utilizadores
                   .FirstOrDefaultAsync(x => x.Nome.ToLower().Equals(credenciais.Nome.ToLower()));

                    utilizador.Nome = credenciais.Nome;
                    utilizador.SenhaString = credenciais.SenhaString;

                if (utilizador == null)
                {
                    throw new System.Exception("Usuário não encontrado.");
                }
                else if (!Criptografia.VerificarPasswordHash(credenciais.SenhaString, utilizador.SenhaHash, utilizador.SenhaSalt))
                {
                    throw new System.Exception("Senha incorreta.");
                }
                else
                {

                    await _context.SaveChangesAsync();

                    return Ok(utilizador);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }

        }



        #region DELETE Utilizador
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizador(int id)
        {
            try
            {
                var utilizador = await _context.Utilizadores.FindAsync(id);
                if (utilizador == null)
                {
                    return NotFound();
                }

                _context.Utilizadores.Remove(utilizador);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir utilizador: {ex.Message}");
            }
        }
        #endregion

        #region Métodos Auxiliares
        private bool UtilizadorExists(int id)
        {
            return _context.Utilizadores.Any(e => e.IdUtilizador == id);
        }
        #endregion
    }
}
