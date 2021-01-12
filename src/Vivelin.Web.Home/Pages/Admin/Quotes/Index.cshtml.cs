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
    public class IndexModel : PageModel
    {
        private readonly Vivelin.Web.Data.DataContext _context;

        public IndexModel(Vivelin.Web.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Quote> Quote { get;set; }

        public async Task OnGetAsync()
        {
            Quote = await _context.Quotes.ToListAsync();
        }
    }
}
