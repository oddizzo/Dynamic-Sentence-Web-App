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
    public class WordTypesController : Controller
    {
        private readonly DatabaseContext _context;

        public WordTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: WordTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WordTypes.ToListAsync());
        }

        // GET: WordTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordType = await _context.WordTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordType == null)
            {
                return NotFound();
            }

            return View(wordType);
        }

        // GET: WordTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WordTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] WordType wordType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wordType);
        }

        // GET: WordTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordType = await _context.WordTypes.FindAsync(id);
            if (wordType == null)
            {
                return NotFound();
            }
            return View(wordType);
        }

        // POST: WordTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] WordType wordType)
        {
            if (id != wordType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordTypeExists(wordType.Id))
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
            return View(wordType);
        }

        // GET: WordTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordType = await _context.WordTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wordType == null)
            {
                return NotFound();
            }

            return View(wordType);
        }

        // POST: WordTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wordType = await _context.WordTypes.FindAsync(id);
            _context.WordTypes.Remove(wordType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordTypeExists(int id)
        {
            return _context.WordTypes.Any(e => e.Id == id);
        }
    }
}
