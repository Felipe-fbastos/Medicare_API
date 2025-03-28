using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Models
{
    [Route("[controller]")]
    [ApiController]
    public class AlarmeController : ControllerBase
    {
        private readonly DataContext _context;

        public AlarmeController(DataContext context)
        {
            _context = context;
        }

        #region GET Alarmes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alarme>>> GetAlarmes()
        {
            try
            {
                var alarmes = await _context.Alarmes.ToListAsync();

                if (alarmes == null || !alarmes.Any())
                    return NotFound();

                return Ok(alarmes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region GET Alarme by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Alarme>> GetAlarme(int id)
        {
            try
            {
                var alarme = await _context.Alarmes.FindAsync(id);
                if (alarme == null)
                {
                    return NotFound();
                }

                return Ok(alarme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region POST Alarme
        [HttpPost]
        public async Task<ActionResult<Alarme>> PostAlarme([FromBody] AlarmeDTO alarmeDTO)
        {
            try
            {
                // 1. Verificar se os Ids foram enviados
                if (alarmeDTO.IdPosologia == 0 || alarmeDTO.IdRemedio == 0)
                {
                    return BadRequest("Os Ids de Posologia e Remédio são obrigatórios.");
                }

                // 2. Buscar as entidades relacionadas (Posologia e Remédio)
                var posologia = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdPosologia == alarmeDTO.IdPosologia);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == alarmeDTO.IdRemedio);

                // 3. Se alguma das entidades não for encontrada, retornar erro
                if (posologia == null || remedio == null)
                {
                    return BadRequest("A Posologia ou o Remédio não existem.");
                }

                // 4. Criar o Alarme
                var alarme = new Alarme(
                    idAlarme: 0, // O ID será gerado automaticamente
                    idPosologia: alarmeDTO.IdPosologia,
                    idRemedio: alarmeDTO.IdRemedio,
                    dtHoraAlarme: alarmeDTO.DtHoraAlarme,
                    stAlarme: alarmeDTO.StAlarme
                );

                // 5. Associar as entidades relacionadas (Posologia e Remédio)
                alarme.Posologia = posologia;
                alarme.Remedio = remedio;

                // 6. Adicionar o novo Alarme ao contexto e salvar
                _context.Alarmes.Add(alarme);
                await _context.SaveChangesAsync();

                // 7. Retornar o Alarme criado
                return CreatedAtAction(nameof(GetAlarme), new { id = alarme.IdAlarme }, alarme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region PUT Alarme
        [HttpPut("{id1}")]
        public async Task<ActionResult<Alarme>> PutAlarme(int id1, [FromBody] AlarmeDTO alarmeDTO)
        {
            try
            {
                
                // 1. Verificar se os Ids foram enviados
                if (alarmeDTO.IdPosologia == 0 || alarmeDTO.IdRemedio == 0)
                {
                    return BadRequest("Os Ids de Posologia e Remédio são obrigatórios.");
                }

                // 2. Buscar o Alarme no banco de dados pelo Id
                var alarme = await _context.Alarmes
                    .FirstOrDefaultAsync(a => a.IdAlarme == id1);

                if (alarme == null)
                {
                    return NotFound("Alarme não encontrado.");
                }

                // 3. Buscar as entidades relacionadas (Posologia e Remédio)
                var posologia = await _context.Posologias
                    .FirstOrDefaultAsync(p => p.IdPosologia == alarmeDTO.IdPosologia);

                var remedio = await _context.Remedios
                    .FirstOrDefaultAsync(r => r.IdRemedio == alarmeDTO.IdRemedio);

                if (posologia == null || remedio == null)
                {
                    return BadRequest("A Posologia ou o Remédio não existem.");
                }

                // 4. Atualizar os dados do Alarme
                alarme.IdPosologia = alarmeDTO.IdPosologia;
                alarme.IdRemedio = alarmeDTO.IdRemedio;
                alarme.DtHoraAlarme = alarmeDTO.DtHoraAlarme;
                alarme.StAlarme = alarmeDTO.StAlarme;

                // 5. Atualizar as entidades relacionadas no Alarme
                alarme.Posologia = posologia;
                alarme.Remedio = remedio;

                // 6. Salvar as mudanças no contexto
                _context.Alarmes.Update(alarme);
                await _context.SaveChangesAsync();

                // 7. Retornar o Alarme atualizado
                return Ok(alarme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region DELETE Alarme
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlarme(int id)
        {
            try
            {
                var alarme = await _context.Alarmes.FindAsync(id);
                if (alarme == null)
                {
                    return NotFound();
                }

                _context.Alarmes.Remove(alarme);
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
