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
    public class WordSentencesController : Controller
    {
        private readonly DatabaseContext _context;

        public WordSentencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: WordSentences
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.WordSentences.Include(w => w.Sentence).Include(w => w.WordUnit);
            return View(await databaseContext.ToListAsync());
        }

        // GET: WordSentences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordSentence = await _context.WordSentences
                .Include(w => w.Sentence)
                .Include(w => w.WordUnit)
                .FirstOrDefaultAsync(m => m.WordUnitId == id);
            if (wordSentence == null)
            {
                return NotFound();
            }

            return View(wordSentence);
        }

        // GET: WordSentences/Create
        public IActionResult Create()
        {
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "Id", "Id");
            ViewData["WordUnitId"] = new SelectList(_context.WordUnits, "Id", "Id");
            return View();
        }

        // POST: WordSentences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordUnitId,SentenceId")] WordSentence wordSentence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wordSentence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "Id", "Id", wordSentence.SentenceId);
            ViewData["WordUnitId"] = new SelectList(_context.WordUnits, "Id", "Id", wordSentence.WordUnitId);
            return View(wordSentence);
        }

        // GET: WordSentences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordSentence = await _context.WordSentences.FindAsync(id);
            if (wordSentence == null)
            {
                return NotFound();
            }
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "Id", "Id", wordSentence.SentenceId);
            ViewData["WordUnitId"] = new SelectList(_context.WordUnits, "Id", "Id", wordSentence.WordUnitId);
            return View(wordSentence);
        }

        // POST: WordSentences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WordUnitId,SentenceId")] WordSentence wordSentence)
        {
            if (id != wordSentence.WordUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wordSentence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordSentenceExists(wordSentence.WordUnitId))
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
            ViewData["SentenceId"] = new SelectList(_context.Sentences, "Id", "Id", wordSentence.SentenceId);
            ViewData["WordUnitId"] = new SelectList(_context.WordUnits, "Id", "Id", wordSentence.WordUnitId);
            return View(wordSentence);
        }

        // GET: WordSentences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordSentence = await _context.WordSentences
                .Include(w => w.Sentence)
                .Include(w => w.WordUnit)
                .FirstOrDefaultAsync(m => m.WordUnitId == id);
            if (wordSentence == null)
            {
                return NotFound();
            }

            return View(wordSentence);
        }

        // POST: WordSentences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wordSentence = await _context.WordSentences.FindAsync(id);
            _context.WordSentences.Remove(wordSentence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordSentenceExists(int id)
        {
            return _context.WordSentences.Any(e => e.WordUnitId == id);
        }
    }
}
