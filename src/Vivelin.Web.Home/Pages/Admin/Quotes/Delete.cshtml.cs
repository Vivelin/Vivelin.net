using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes;

public class DeleteModel : PageModel
{
    private readonly DataContext _context;

    public DeleteModel(DataContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Quote? Quote { get; set; }

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Quote = await _context.Quotes.FindAsync(id);

        if (Quote != null)
        {
            _context.Quotes.Remove(Quote);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
