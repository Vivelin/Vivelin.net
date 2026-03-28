using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Vivelin.Web.Home;

public static class UrlHelperExtensions
{
    public static string? AbsoluteContent(this IUrlHelper url, string? path)
    {
        if (path == null)
        {
            return null;
        }

        try
        {
            Uri baseUrl = new(url.ActionContext.HttpContext.Request.GetEncodedUrl());
            var contentPath = url.Content(path);
            Uri absoluteUriToContent = new(baseUrl, contentPath);
            return absoluteUriToContent.ToString();
        }
        catch { }

        return path;
    }
}
