using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentDatabase.Models;

namespace StudentDatabase.Controllers
{
    public class StudentDatumsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentDatumsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: StudentDatums
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentData.ToListAsync());
        }

        // GET: StudentDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDatum = await _context.StudentData
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentDatum == null)
            {
                return NotFound();
            }

            return View(studentDatum);
        }

        // GET: StudentDatums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,StudentAge")] StudentDatum studentDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentDatum);
        }

        // GET: StudentDatums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDatum = await _context.StudentData.FindAsync(id);
            if (studentDatum == null)
            {
                return NotFound();
            }
            return View(studentDatum);
        }

        // POST: StudentDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,StudentAge")] StudentDatum studentDatum)
        {
            if (id != studentDatum.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentDatumExists(studentDatum.StudentId))
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
            return View(studentDatum);
        }

        // GET: StudentDatums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDatum = await _context.StudentData
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentDatum == null)
            {
                return NotFound();
            }

            return View(studentDatum);
        }

        // POST: StudentDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentDatum = await _context.StudentData.FindAsync(id);
            if (studentDatum != null)
            {
                _context.StudentData.Remove(studentDatum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentDatumExists(int id)
        {
            return _context.StudentData.Any(e => e.StudentId == id);
        }
    }
}
