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
    public class JeloesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JeloesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jeloes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jelo.Include(j => j.Restoran);
            return View(await applicationDbContext.ToListAsync());
        }


        // popravljena random metoda sa linq 
        // GET: Jeloes/Random
        public async Task<IActionResult> Random()
        {
            Random r = new Random();
            return View("Random2", await Task.FromResult(_context.Jelo.Include("Restoran").Skip(r.Next(_context.Jelo.ToList().Count)).Take(1).AsEnumerable()));
        }



        // random metoda na ovaj način radi, ali svjestan sam da nije baš efikasna
        // GET: Jeloes/Random2
        public ActionResult Random2()
        {
            Random r = new Random();
            List<Jelo> list = new List<Jelo>();
            list = _context.Jelo.Include("Restoran").ToList();
            int c = list.Count;
            
            Jelo j = new Jelo();
            j = list.ElementAt(r.Next(c));
            
            list.Clear();
            
            list.Add(j);
            return View(  list);
        }


        // random metoda sa sqlraw upitom ne radi
        // GET: Jeloes/Random3
        public async Task<IActionResult> Random3()
        {
            return View("Index", await _context.Jelo.FromSqlRaw("select top 1 Jelo.Naziv, Jelo.Cijena, Restoran.Naziv AS Restoran, Restoran.Adresa, Jelo.RestoranID, Restoran.ID from Jelo INNER JOIN Restoran ON Jelo.RestoranID = Restoran.ID order by newid()").ToListAsync());
        }



        // GET: Jeloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .Include(j => j.Restoran)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // GET: Jeloes/Create
        public IActionResult Create()
        {
            ViewData["RestoranID"] = new SelectList(_context.Restoran, "ID", "Naziv");
            return View();
        }

        // POST: Jeloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Cijena,RestoranID")] Jelo jelo)
        {
           
            
                _context.Add(jelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["RestoranID"] = new SelectList(_context.Restoran, "ID", "ID", jelo.RestoranID);
            return View(jelo);
        }

        // GET: Jeloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo.FindAsync(id);
            if (jelo == null)
            {
                return NotFound();
            }
            ViewData["RestoranID"] = new SelectList(_context.Restoran, "ID", "Naziv", jelo.RestoranID);
            return View(jelo);
        }

        // POST: Jeloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Cijena,RestoranID")] Jelo jelo)
        {
            if (id != jelo.ID)
            {
                return NotFound();
            }

           
            {
                try
                {
                    _context.Update(jelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JeloExists(jelo.ID))
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
            ViewData["RestoranID"] = new SelectList(_context.Restoran, "ID", "ID", jelo.RestoranID);
            return View(jelo);
        }

        // GET: Jeloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jelo == null)
            {
                return NotFound();
            }

            var jelo = await _context.Jelo
                .Include(j => j.Restoran)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }

        // POST: Jeloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Jelo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Jelo'  is null.");
            }
            var jelo = await _context.Jelo.FindAsync(id);
            if (jelo != null)
            {
                _context.Jelo.Remove(jelo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JeloExists(int id)
        {
          return _context.Jelo.Any(e => e.ID == id);
        }
    }
}
