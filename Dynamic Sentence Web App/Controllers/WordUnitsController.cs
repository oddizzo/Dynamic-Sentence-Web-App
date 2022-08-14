using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dynamic_Sentence_Web_App.Database;
using Dynamic_Sentence_Web_App.Models;

namespace Dynamic_Sentence_Web_App.Controllers
{
    public class WordUnitsController : Controller
    {
        private readonly DatabaseContext _context;

        public WordUnitsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: WordUnits
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.WordUnits.Include(w => w.WordType);
            return View(await databaseContext.ToListAsync());
        }

        // GET: WordUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordUnit = await _context.WordUnits
                .Include(w => w.WordType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordUnit == null)
            {
                return NotFound();
            }

            return View(wordUnit);
        }

        // GET: WordUnits/Create
        public IActionResult Create()
        {
            ViewData["WordType"] = new SelectList(_context.WordTypes, "Id", "Type");
            return View();
        }

        // POST: WordUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Word,WordTypeId")] WordUnit wordUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WordTypeId"] = new SelectList(_context.WordTypes, "Id", "Id", wordUnit.WordTypeId);
            return View(wordUnit);
        }

        // GET: WordUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordUnit = await _context.WordUnits.FindAsync(id);
            if (wordUnit == null)
            {
                return NotFound();
            }
            ViewData["WordType"] = new SelectList(_context.WordTypes, "Id", "Type", wordUnit.WordType);
            return View(wordUnit);
        }

        // POST: WordUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Word,WordTypeId")] WordUnit wordUnit)
        {
            if (id != wordUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordUnitExists(wordUnit.Id))
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
            ViewData["WordTypeId"] = new SelectList(_context.WordTypes, "Id", "Id", wordUnit.WordTypeId);
            return View(wordUnit);
        }

        // GET: WordUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordUnit = await _context.WordUnits
                .Include(w => w.WordType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordUnit == null)
            {
                return NotFound();
            }

            return View(wordUnit);
        }

        // POST: WordUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wordUnit = await _context.WordUnits.FindAsync(id);
            _context.WordUnits.Remove(wordUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordUnitExists(int id)
        {
            return _context.WordUnits.Any(e => e.Id == id);
        }
    }
}
