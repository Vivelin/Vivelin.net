using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vivelin.Web.Home.TagHelpers
{
    /// <summary>
    /// Defines the supported markup languages used to write content in.
    /// </summary>
    public enum MarkupLanguage
    {
        None = 0,

        /// <summary>
        /// Renders content written in CommonMark or Markdown.
        /// </summary>
        CommonMark
    }
}
