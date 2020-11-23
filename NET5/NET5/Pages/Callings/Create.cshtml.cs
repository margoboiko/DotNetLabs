using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NET5.Models;

namespace NET5.Pages.Callings
{
    public class CreateModel : PageModel
    {
        private readonly NET5.Models.phone_callContext _context;

        public CreateModel(NET5.Models.phone_callContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Cityid"] = new SelectList(_context.City, "Id", "Id");
        ViewData["Personid"] = new SelectList(_context.Person, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Calling Calling { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Calling.Add(Calling);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
