using System;
using System.Security.Policy;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Vivelin.Web.Home;

public static class UrlHelperExtensions
{
    public static string? AbsoluteContent(this IUrlHelper url, string? path)
    {
        if (path == null)
            return null;

        try
        {
            var baseUrl = new Uri(url.ActionContext.HttpContext.Request.GetEncodedUrl());
            var contentPath = url.Content(path);
            var absoluteUriToContent = new Uri(baseUrl, contentPath);
            return absoluteUriToContent.ToString();
        }
        catch { }

        return path;
    }
}
