using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class FormaPagamento : ControllerBase
    {
        private readonly DataContext _context;

        public FormaPagamento(DataContext context)
        {
            _context = context;
        }

          private bool AlarmeExistente(int id)
        {
            return _context.ALARMES.Any(a => a.Id == id);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAlarmes()
        {
            try
            {
                List<Alarme> lista = await _context.ALARMES.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizador>> GetAlarmeById(int id){

             try
            {
                return Ok (await _context.ALARMES.FindAsync(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }

           
        }


        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarAlarme(Alarme alarme)
        {
            try
            {
                await _context.ALARMES.AddAsync(alarme);
                await _context.SaveChangesAsync();

                return Ok(alarme.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlarme(int id, Alarme alarme)
        {
            if (id != alarme.Id)
            {
                return BadRequest();
            }

            _context.Entry(alarme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlarmeExistente(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
         [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlarme(int id)
        {
            var alarme = await _context.ALARMES.FindAsync(id);
            if (alarme == null)
            {
                return NotFound();
            }

            _context.ALARMES.Remove(alarme);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
    
    }
}