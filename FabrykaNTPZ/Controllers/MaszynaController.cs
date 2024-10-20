using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FabrykaNTPZ.Data;
using FabrykaNTPZ.Models;

namespace FabrykaNTPZ.Controllers
{
    public class MaszynaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MaszynaController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Maszyna
        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var lista = _db.Maszyny.Include(m => m.Hala).OrderBy(m => m.Nazwa);
                return View(await lista.ToListAsync());
            }
            else
            {
                var lista = _db.Maszyny.Include(m => m.Hala).Where(m => m.Nazwa.Contains(search)).OrderBy(m => m.Nazwa);
                return View(await lista.ToListAsync());
            }
        }

        // GET: Maszyna/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maszyna = await _db.Maszyny.Include(m => m.Hala).FirstOrDefaultAsync(m => m.Id == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return View(maszyna);
        }

        // GET: Maszyna/Create
        public IActionResult Create()
        {
            ViewData["HalaId"] = new SelectList(_db.Hale, "Id", "Nazwa");
            return View();
        }

        // POST: Maszyna/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (ModelState.IsValid)
            {
                _db.Add(maszyna);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HalaId"] = new SelectList(_db.Hale, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maszyna = await _db.Maszyny.FindAsync(id);
            if (maszyna == null)
            {
                return NotFound();
            }
            ViewData["HalaId"] = new SelectList(_db.Hale, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // POST: Maszyna/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (id != maszyna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(maszyna);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaszynaExists(maszyna.Id))
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
            ViewData["HalaId"] = new SelectList(_db.Hale, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maszyna = await _db.Maszyny
                .Include(m => m.Hala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return View(maszyna);
        }

        // POST: Maszyna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maszyna = await _db.Maszyny.FindAsync(id);
            if (maszyna != null)
            {
                _db.Maszyny.Remove(maszyna);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaszynaExists(int id)
        {
            return _db.Maszyny.Any(e => e.Id == id);
        }
    }
}
