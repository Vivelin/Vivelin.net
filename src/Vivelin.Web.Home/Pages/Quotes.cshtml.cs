using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages;

public class QuoteModel(DataContext context) : PageModel
{
    [BindProperty]
    public Quote Quote { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        Quote = (await context.Quotes.SingleOrDefaultAsync(m => m.Id == id, cancellationToken))!;
        if (Quote == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"Quote #{id}";
        return Page();
    }
}
