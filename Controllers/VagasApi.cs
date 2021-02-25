using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Empregare.Data;
using Empregare.Models;

namespace Empregare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagasApi : ControllerBase
    {
        private readonly Context _context;

        public VagasApi(Context context)
        {
            _context = context;
        }

        // GET: api/VagasApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaga>>> GetVaga()
        {
            return await _context.Vaga.ToListAsync();
        }

        // GET: api/VagasApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaga>> GetVaga(int id)
        {
            var vaga = await _context.Vaga.FindAsync(id);

            if (vaga == null)
            {
                return NotFound();
            }

            return vaga;
        }

        // PUT: api/VagasApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaga(int id, Vaga vaga)
        {
            if (id != vaga.Id)
            {
                return BadRequest();
            }

            _context.Entry(vaga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VagaExists(id))
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

        // POST: api/VagasApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vaga>> PostVaga(Vaga vaga)
        {
            _context.Vaga.Add(vaga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaga", new { id = vaga.Id }, vaga);
        }

        // DELETE: api/VagasApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaga(int id)
        {
            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }

            _context.Vaga.Remove(vaga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VagaExists(int id)
        {
            return _context.Vaga.Any(e => e.Id == id);
        }
    }
}
