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
    public class SentencesController : Controller
    {
        private readonly DatabaseContext _context;

        public SentencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Sentences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sentences.ToListAsync());
        }

        // GET: Sentences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sentence == null)
            {
                return NotFound();
            }

            return View(sentence);
        }

        // GET: Sentences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sentences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Words")] Sentence sentence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sentence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sentence);
        }

        // GET: Sentences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }
            return View(sentence);
        }

        // POST: Sentences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Words")] Sentence sentence)
        {
            if (id != sentence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sentence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SentenceExists(sentence.Id))
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
            return View(sentence);
        }

        // GET: Sentences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sentence = await _context.Sentences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sentence == null)
            {
                return NotFound();
            }

            return View(sentence);
        }

        // POST: Sentences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sentence = await _context.Sentences.FindAsync(id);
            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.Id == id);
        }
    }
}
