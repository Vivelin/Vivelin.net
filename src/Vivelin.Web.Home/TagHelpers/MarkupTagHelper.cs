using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Markdig;
using Markdig.Extensions.AutoIdentifiers;
using Markdig.Extensions.SmartyPants;
using Markdig.Prism;
using Markdig.Renderers;
using Markdig.Renderers.Html.Inlines;

using Microsoft.AspNetCore.Razor.TagHelpers;

using Vivelin.Web.Home.MarkdownExtensions;

namespace Vivelin.Web.Home.TagHelpers
{
    /// <summary>
    /// A TagHelper that renders HTML based on the specified markup language.
    /// </summary>
    [HtmlTargetElement("pre", Attributes = "[markup-language]")]
    public class MarkupTagHelper : TagHelper
    {
        public MarkupLanguage MarkupLanguage { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (MarkupLanguage == MarkupLanguage.None)
                return;

            var content = await output.GetChildContentAsync();
            var markup = content.GetContent();
            var html = MarkupLanguage switch
            {
                MarkupLanguage.CommonMark => RenderCommonMark(markup),
                MarkupLanguage.None => throw new UnreachableException(),
                _ => throw new InvalidEnumArgumentException(nameof(MarkupLanguage), (int)MarkupLanguage, typeof(MarkupLanguage))
            };

            output.TagName = null;
            output.Content.SetHtmlContent(html);
        }

        protected virtual string RenderCommonMark(string markup)
        {
            var pipeline = new MarkdownPipelineBuilder()
                .UseAutoIdentifiers()
                .Use(new LinkTargetExtension())
                .Build();

            return Markdown.ToHtml(markup, pipeline);
        }
    }
}
