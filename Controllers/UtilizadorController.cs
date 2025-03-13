using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UtilizadorController : ControllerBase
    {
        private readonly DataContext _context;

        public UtilizadorController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> EmailExistente(string email)
        {
            if (await _context.UTILIZADOR.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
                return true;
            else
                return false;
        }

          private bool UtilizadorExistente(int id)
        {
            return _context.UTILIZADOR.Any(u => u.Id == id);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUtilizadores()
        {
            try
            {
                List<Utilizador> lista = await _context.UTILIZADOR.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizador>> GetById(int id){

             try
            {
                return Ok (await _context.UTILIZADOR.FindAsync(id));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }

           
        }


        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUtilizador(Utilizador utilizador)
        {
            try
            {
                if (await EmailExistente(utilizador.Email)) 
                    throw new System.Exception("O Email do Utilizador já existe");

                await _context.UTILIZADOR.AddAsync(utilizador);
                await _context.SaveChangesAsync();

                return Ok(utilizador.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizador(int id, Utilizador utilizador)
        {
            if (id != utilizador.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilizador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizadorExistente(id))
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
        public async Task<IActionResult> DeleteUtilizador(int id)
        {
            var utilizador = await _context.UTILIZADOR.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            _context.UTILIZADOR.Remove(utilizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
    
    }
}