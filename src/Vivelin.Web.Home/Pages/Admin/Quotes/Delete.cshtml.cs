using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes
{
    public class DeleteModel : PageModel
    {
        private readonly Vivelin.Web.Data.DataContext _context;

        public DeleteModel(Vivelin.Web.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote? Quote { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            Quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

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

            Quote = await _context.Quotes.FindAsync([id], cancellationToken);

            if (Quote != null)
            {
                _context.Quotes.Remove(Quote);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return RedirectToPage("./Index");
        }
    }
}
