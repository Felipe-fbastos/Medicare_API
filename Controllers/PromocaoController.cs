using Medicare_API.Data;
using Medicare_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocao>>> GetPromocoes()
        {
            
            var promocoes = await _context.Promocoes.ToListAsync();
            if (promocoes == null)
            {
                return NotFound();
            }

            return Ok(promocoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Promocao>> GetPromocao(int id)
        {
            var promocaoList = await _context.Promocoes.FindAsync(id);
            if (promocaoList == null)
            {
                return NotFound();
            }

            return Ok(promocaoList);
        }

        [HttpPost]
        public async Task<ActionResult<Promocao>> PostPromocao(Promocao promocao)
        {
            _context.Promocoes.Add(promocao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPromocao), new { id = promocao.IdPromocao }, promocao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromocao(int id, Promocao promocao)
        {
            if (id != promocao.IdPromocao)
            {
                return BadRequest();
            }

            _context.Entry(promocao).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromocao(int id)
        {
            var promocao = await _context.Promocoes.FindAsync(id);
            if (promocao == null)
            {
                return NotFound();
            }

            _context.Promocoes.Remove(promocao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
