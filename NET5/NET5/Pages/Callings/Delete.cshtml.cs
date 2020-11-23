using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NET5.Models;

namespace NET5.Pages.Callings
{
    public class DeleteModel : PageModel
    {
        private readonly NET5.Models.phone_callContext _context;

        public DeleteModel(NET5.Models.phone_callContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Calling = await _context.Calling.FindAsync(id);

            if (Calling != null)
            {
                _context.Calling.Remove(Calling);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
