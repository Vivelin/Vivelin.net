using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home.Pages.Admin.Quotes
{
    public class EditModel : PageModel
    {
        private readonly Vivelin.Web.Data.DataContext _context;

        public EditModel(Vivelin.Web.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote Quote { get; set; }

        public bool IsNew => Quote == null || Quote.Id == default;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                ViewData["Title"] = "Add quote";
                return Page();
            }

            Quote = await _context.Quotes.FirstOrDefaultAsync(m => m.Id == id);
            if (Quote == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Edit quote";
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Quote == null || !ModelState.IsValid)
            {
                return Page();
            }

            if (Quote.Id == default)
                _context.Quotes.Add(Quote);
            else
                _context.Attach(Quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Quotes.AnyAsync(e => e.Id == Quote.Id))
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
}
