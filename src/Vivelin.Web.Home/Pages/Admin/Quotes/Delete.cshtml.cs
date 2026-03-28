using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes;

public class DeleteModel(DataContext context) : PageModel
{
    [BindProperty]
    public Quote? Quote { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        Quote = await context.Quotes.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (Quote == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            return NotFound();
        }

        Quote = await context.Quotes.FindAsync([id], cancellationToken);

        if (Quote != null)
        {
            context.Quotes.Remove(Quote);
            await context.SaveChangesAsync(cancellationToken);
        }

        return RedirectToPage("./Index");
    }
}
