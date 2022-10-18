using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DostavaHrane.Data;
using DostavaHrane.Models;

namespace DostavaHrane.Controllers
{
    public class RestoransController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestoransController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restorans
        public async Task<IActionResult> Index()
        {
              return View(await _context.Restoran.ToListAsync());
        }

        // GET: Restorans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // GET: Restorans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restorans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Adresa,Telefon")] Restoran restoran)
        {
           
            
                _context.Add(restoran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
                return View(restoran);
        }

        // GET: Restorans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran.FindAsync(id);
            if (restoran == null)
            {
                return NotFound();
            }
            return View(restoran);
        }

        // POST: Restorans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Adresa,Telefon")] Restoran restoran)
        {
            if (id != restoran.ID)
            {
                return NotFound();
            }

            
            {
                try
                {
                    _context.Update(restoran);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestoranExists(restoran.ID))
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
            return View(restoran);
        }

        // GET: Restorans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Restoran == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restoran
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // POST: Restorans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Restoran == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Restoran'  is null.");
            }
            var restoran = await _context.Restoran.FindAsync(id);
            if (restoran != null)
            {
                _context.Restoran.Remove(restoran);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestoranExists(int id)
        {
          return _context.Restoran.Any(e => e.ID == id);
        }
    }
}
