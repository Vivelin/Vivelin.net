using System;

using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Vivelin.Web.Home.MarkdownExtensions
{
    public class LinkTargetExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            renderer.ObjectWriteBefore += (_, obj) =>
            {
                if (obj is LinkInline or AutolinkInline)
                    obj.GetAttributes().AddPropertyIfNotExist("target", "_blank");
            };
        }
    }
}
