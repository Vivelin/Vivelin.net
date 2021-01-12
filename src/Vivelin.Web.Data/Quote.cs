using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivelin.Web.Data
{
    public class Quote
    {
        [Obsolete("This constructor is only provided for Model Binding compatibility and should not be used.")]
        [DebuggerHidden]
        public Quote() : this(null!, null!)
        {
        }

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
        [DisplayName("Quote")]
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
        [DisplayName("Source URL")]
        public Uri? SourceHref { get; set; }

        /// <summary>
        /// Gets or sets the name of the publisher, if any,
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// Gets or sets the page number on which the quote can be found.
        /// </summary>
        [DisplayName("Page number")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the year of publication, if any.
        /// </summary>
        [DisplayName("Publication year")]
        public string? PublicationYear { get; set; }
    }
}
