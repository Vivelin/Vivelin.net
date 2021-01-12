using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivelin.Web.Data
{
    public class BookQuote : Quote
    {
        public BookQuote(string text, string source) 
            : base(text, source)
        {
        }

        public string? Publisher { get; set; }

        public int? PageNumber { get; set; }

        public string PublicationYear { get; set; }
    }
}
