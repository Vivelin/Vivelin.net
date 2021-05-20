using System;
using System.Globalization;

namespace Vivelin.Web.Home
{
    /// <summary>
    /// Provides additional methods for interacting with dates and times.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the time interval to an approximate string representation.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> to represent.</param>
        /// <returns>
        /// A string containing an approximate representation of <paramref
        /// name="value"/>, e.g. "2 hours".
        /// </returns>
        public static string ToApproximateString(this TimeSpan value)
            => value.ToApproximateString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Converts the time interval to an approximate string representation.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> to represent.</param>
        /// <param name="formatProvider">
        /// Used to specify culture-specific formatting information.
        /// </param>
        /// <returns>
        /// A string containing an approximate representation of <paramref
        /// name="value"/>, e.g. "2 hours".
        /// </returns>
        public static string ToApproximateString(this TimeSpan value, IFormatProvider formatProvider)
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
            return component.ToString(formatProvider);
        }
    }
}