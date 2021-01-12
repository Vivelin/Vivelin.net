using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivelin.Web.Data
{
    public class Quote
    {
        public Quote(string text, string source)
        {
            Text = text;
            Source = source;
        }

        /// <summary>
        /// Gets or sets a unique identifier for the quote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text being quoted.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the name of the author or person that said the quote.
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// Gets or sets the source of the quote.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets a link to the source of the quote.
        /// </summary>
        public Uri? SourceHref { get; set; }
    }
}
