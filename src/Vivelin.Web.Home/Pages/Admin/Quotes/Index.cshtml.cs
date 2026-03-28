using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes;

public class IndexModel(DataContext context) : PageModel
{
    public IList<Quote> Quote { get; set; } = [];

    public async Task OnGetAsync()
    {
        Quote = await context.Quotes.ToListAsync();
    }
}
