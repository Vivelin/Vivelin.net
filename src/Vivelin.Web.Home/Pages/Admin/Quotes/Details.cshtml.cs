using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes
{
    public class DetailsModel : PageModel
    {
        private readonly Vivelin.Web.Data.DataContext _context;

        public DetailsModel(Vivelin.Web.Data.DataContext context)
        {
            _context = context;
        }

        public Quote Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id);

            if (Quote == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
