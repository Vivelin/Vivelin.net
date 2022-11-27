using System;

using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Vivelin.Web.Home.MarkdownExtensions;

public class LinkTargetExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        renderer.ObjectWriteBefore += (_, obj) =>
        {
            if (IsAbsoluteLink(obj))
                obj.GetAttributes().AddPropertyIfNotExist("target", "_blank");

            if (obj is AutolinkInline)
                obj.GetAttributes().AddClass("autolink");
        };
    }

    private static bool IsAbsoluteLink(MarkdownObject obj)
    {
        return obj switch
        {
            LinkInline { Url: var url } when IsAbsoluteUrl(url) => true,
            AutolinkInline { Url: var url } when IsAbsoluteUrl(url) => true,
            _ => false
        };
    }

    private static bool IsAbsoluteUrl(string? url)
        => url != null && (url.StartsWith("http://") || url.StartsWith("https://") || url.StartsWith("//"));
}
