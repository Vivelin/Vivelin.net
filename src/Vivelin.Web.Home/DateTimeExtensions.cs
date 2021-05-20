using System;
using System.Globalization;
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
        /// <returns>
        /// A string containing a relative representation of the <paramref
        /// name="value"/>, e.g. "2 hours ago".
        /// </returns>
        public static string ToRelativeString(this TimeSpan value)
            => value.ToRelativeString(CultureInfo.InvariantCulture);

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
            FormattableString component = value.Duration() switch
            {
                { TotalSeconds: 1 } x => $"1 second",
                { TotalSeconds: < 60 } x => $"{Math.Round(x.TotalSeconds)} seconds",
                { TotalMinutes: 1 } x => $"1 minute",
                { TotalMinutes: < 5 } x => $"{x.TotalMinutes:0.#} minutes",
                { TotalMinutes: < 60 } x => $"{Math.Round(x.TotalMinutes)} minutes",
                { TotalHours: 1 } x => $"1 hour",
                { TotalHours: < 4 } x => $"{x.TotalHours:0.#} hours",
                { TotalHours: < 48 } x => $"{Math.Round(x.TotalHours)} hours",
                { TotalDays: 1 } x => $"1 day",
                { TotalDays: < 5 } x => $"{x.TotalDays:0.#} days",
                TimeSpan x => $"{Math.Round(x.TotalDays)} days"
            };

            if (value.Ticks < 0)
                return $"in {component.ToString(formatProvider)}";
            return $"{component.ToString(formatProvider)} ago";
        }
    }
}