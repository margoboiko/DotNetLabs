using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET4.Models;

namespace NET4.Controllers
{
    public class CallingsController : Controller
    {
        private readonly phone_callContext _context;

        public CallingsController(phone_callContext context)
        {
            _context = context;
        }

        // GET: Callings
        public async Task<IActionResult> Index()
        {
            var phone_callContext = _context.Calling.Include(c => c.City).Include(c => c.Person);
            return View(await phone_callContext.ToListAsync());
        }

        // GET: Callings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling
                .Include(c => c.City)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calling == null)
            {
                return NotFound();
            }

            return View(calling);
        }

        // GET: Callings/Create
        public IActionResult Create()
        {
            ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id");
            ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id");
            return View();
        }

        // POST: Callings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,During,Personid,Cityid")] Calling calling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id", calling.Cityid);
            ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id", calling.Personid);
            return View(calling);
        }

        // GET: Callings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling.FindAsync(id);
            if (calling == null)
            {
                return NotFound();
            }
            ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id", calling.Cityid);
            ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id", calling.Personid);
            return View(calling);
        }

        // POST: Callings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,During,Personid,Cityid")] Calling calling)
        {
            if (id != calling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallingExists(calling.Id))
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
            ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id", calling.Cityid);
            ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id", calling.Personid);
            return View(calling);
        }

        // GET: Callings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calling = await _context.Calling
                .Include(c => c.City)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calling == null)
            {
                return NotFound();
            }

            return View(calling);
        }

        // POST: Callings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calling = await _context.Calling.FindAsync(id);
            _context.Calling.Remove(calling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CallingExists(int id)
        {
            return _context.Calling.Any(e => e.Id == id);
        }
    }
}
