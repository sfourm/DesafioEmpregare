using Empregare.Data;
using Empregare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Empregare.Controllers
{
    public class VagasController : Controller
    {
        private readonly Context _context;

        public VagasController(Context context)
        {
            _context = context;
        }

        // GET: Vagas
        public async Task<IActionResult> Vagas()
        {
            return View(await _context.Vaga.ToListAsync());
        }

        // GET: Vagas/Details/5
        public async Task<IActionResult> DetalhesVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // GET: Vagas/Create
        public IActionResult CriarVaga()
        {
            return View();
        }

        // POST: Vagas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarVaga([Bind("Id,Cargo,Localizacao,Salario,Data")] Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaga);
        }

        // GET: Vagas/Edit/5
        public async Task<IActionResult> EditarVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga.FindAsync(id);
            if (vaga == null)
            {
                return NotFound();
            }
            return View(vaga);
        }

        // POST: Vagas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarVaga(int id, [Bind("Id,Cargo,Localizacao,Salario,Data")] Vaga vaga)
        {
            if (id != vaga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagaExiste(vaga.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaga);
        }

        // GET: Vagas/Delete/5
        public async Task<IActionResult> DeletarVaga(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaga = await _context.Vaga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaga == null)
            {
                return NotFound();
            }

            return View(vaga);
        }

        // POST: Vagas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarDeletarVaga(int id)
        {
            var vaga = await _context.Vaga.FindAsync(id);
            _context.Vaga.Remove(vaga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VagaExiste(int id)
        {
            return _context.Vaga.Any(e => e.Id == id);
        }

    }
}
