using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vivelin.Web.Home.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel(IWebHostEnvironment environment) : PageModel
{
    public string? ExceptionMessage { get; set; }

    public bool ShowDetails => environment.IsDevelopment();

    public void OnGet()
    {
        if (environment.IsDevelopment())
        {
            IExceptionHandlerPathFeature? exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandler != null)
            {
                ExceptionMessage = exceptionHandler.Error.ToString();
            }
        }
    }

    public void OnPost()
    {
        if (environment.IsDevelopment())
        {
            IExceptionHandlerPathFeature? exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandler != null)
            {
                ExceptionMessage = exceptionHandler.Error.ToString();
            }
        }
    }
}