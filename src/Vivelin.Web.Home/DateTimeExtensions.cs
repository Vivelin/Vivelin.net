using System;
using System.Security.Cryptography.X509Certificates;

namespace Vivelin.Web.Home
{
    /// <summary>
    /// Provides additional methods for interacting with dates and times.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a string representing the time interval as a time relative
        /// to the current time.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> to represent.</param>
        /// <param name="formatProvider">
        /// Used to specify culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A string containing a relative representation of the <paramref
        /// name="value"/>, e.g. "2 hours ago".
        /// </returns>
        public static string ToRelativeString(this TimeSpan value, IFormatProvider formatProvider)
        {
            FormattableString result = value switch
            {
                { TotalSeconds: < 60 } => $"{Math.Round(value.TotalSeconds)} seconds ago",
                { TotalMinutes: < 5 } => $"{value.TotalMinutes:0.#} minutes ago",
                { TotalMinutes: < 60 } => $"{Math.Round(value.TotalMinutes)} minutes ago",
                { TotalHours: < 4 } => $"{value.TotalHours:0.#} hours ago",
                { TotalHours: < 48 } => $"{Math.Round(value.TotalHours)} hours ago",
                { TotalDays: < 5 } => $"{value.TotalDays:0.#} days ago",
                _ => $"{Math.Round(value.TotalDays)} days ago"
            };

            return result.ToString(formatProvider);
        }
    }
}