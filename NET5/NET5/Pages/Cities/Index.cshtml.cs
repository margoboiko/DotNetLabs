using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NET5.Models;

namespace NET5.Pages.Cities
{
    public class IndexModel : PageModel
    {
        private readonly NET5.Models.phone_callContext _context;

        public IndexModel(NET5.Models.phone_callContext context)
        {
            _context = context;
        }

        public IList<City> City { get;set; }

        public async Task OnGetAsync()
        {
            City = await _context.City.ToListAsync();
        }
    }
}
