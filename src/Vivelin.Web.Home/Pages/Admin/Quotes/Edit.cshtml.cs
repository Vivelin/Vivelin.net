using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes;

public class EditModel(DataContext context) : PageModel
{
    [BindProperty]
    public Quote? Quote { get; set; }

    public bool IsNew => Quote == null || Quote.Id == default;

    public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
    {
        if (id == null)
        {
            ViewData["Title"] = "Add quote";
            return Page();
        }

        Quote = await context.Quotes.SingleOrDefaultAsync(m => m.Id == id, cancellationToken);
        if (Quote == null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Edit quote";
        return Page();
    }

    // To protect from overposting attacks, see
    // https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (IsNew)
        {
            ModelState["Quote.Id"]?.ValidationState = ModelValidationState.Skipped;
        }

        if (Quote == null || !ModelState.IsValid)
        {
            return Page();
        }

        if (Quote.Id == default)
        {
            context.Quotes.Add(Quote);
        }
        else
        {
            context.Attach(Quote).State = EntityState.Modified;
        }

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await context.Quotes.AnyAsync(e => e.Id == Quote.Id, cancellationToken))
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
}
