using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vivelin.Web.Home
{
    /// <summary>
    /// Represents errors that occur when unreachable code is reached.
    /// </summary>
    public class UnreachableException : Exception
    {
        public UnreachableException() 
            : this("Reached unreachable code.")
        {
        }

        public UnreachableException(string? message) 
            : base(message)
        {
        }

        public UnreachableException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}
