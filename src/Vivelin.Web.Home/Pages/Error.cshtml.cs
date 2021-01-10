using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;

namespace Vivelin.Web.Home.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly IWebHostEnvironment _environment;

        public string RequestId { get; set; }

        public string ExceptionMessage { get; set; }

        public bool ShowDetails => _environment.IsDevelopment();

        public ErrorModel(ILogger<ErrorModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            if (_environment.IsDevelopment())
            {
                var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if (exceptionHandler != null)
                {
                    ExceptionMessage = exceptionHandler.Error.ToString();
                }
            }
        }
    }
}