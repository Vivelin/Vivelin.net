using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vivelin.Web.Home.Pages;

public class IndexModel() : PageModel
{
    public void OnGet()
    {

    }

    public IActionResult OnGetLogin()
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated)
        {
            return Challenge();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync();
        return RedirectToPage();
    }
}
