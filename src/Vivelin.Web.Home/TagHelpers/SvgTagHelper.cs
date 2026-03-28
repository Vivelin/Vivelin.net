using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System.Text.Encodings.Web;

namespace Vivelin.Web.Home.TagHelpers;

/// <summary>
/// A TagHelper that renders an SVG icon inline.
/// </summary>
[HtmlTargetElement("svg-icon", Attributes = "src", TagStructure = TagStructure.WithoutEndTag)]
public class SvgTagHelper(IWebHostEnvironment environment, HtmlEncoder htmlEncoder) : TagHelper
{
    [HtmlAttributeName("src")]
    public string? Src { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrEmpty(Src))
        {
            output.SuppressOutput();
            return;
        }

        var path = GetPath(Src);
        var svg = await File.ReadAllTextAsync(path);
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.AddClass("svg-icon", htmlEncoder);
        output.Content.SetHtmlContent(svg);
    }

    private string GetPath(string src)
    {
        if (Path.IsPathRooted(src))
        {
            return src;
        }

        if (src.StartsWith('~'))
        {
            return Path.Combine(environment.WebRootPath, src[2..]);
        }

        return Path.Combine(environment.ContentRootPath, src);
    }
}
