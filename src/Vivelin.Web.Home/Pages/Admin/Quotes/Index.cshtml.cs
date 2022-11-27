using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes;

public class IndexModel : PageModel
{
    private readonly DataContext _context;

    public IndexModel(DataContext context)
    {
        _context = context;
    }

    public IList<Quote> Quote { get; set; } = new List<Quote>();

    public async Task OnGetAsync()
    {
        Quote = await _context.Quotes.ToListAsync();
    }
}
