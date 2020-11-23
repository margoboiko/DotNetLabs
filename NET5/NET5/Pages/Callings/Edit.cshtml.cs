using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET5.Models;

namespace NET5.Pages.Callings
{
    public class EditModel : PageModel
    {
        private readonly NET5.Models.phone_callContext _context;

        public EditModel(NET5.Models.phone_callContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Calling Calling { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calling = await _context.Calling
                .Include(c => c.City)
                .Include(c => c.Person).FirstOrDefaultAsync(m => m.Id == id);

            if (Calling == null)
            {
                return NotFound();
            }
           ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id");
           ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Calling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallingExists(Calling.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CallingExists(int id)
        {
            return _context.Calling.Any(e => e.Id == id);
        }
    }
}
