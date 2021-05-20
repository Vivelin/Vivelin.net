using System;
using System.Globalization;

using Vivelin.Web.Home;

using Xunit;

namespace Vivelin.Web.Tests
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("00:00:05", "5 seconds")]
        [InlineData("00:05:00", "5 minutes")]
        [InlineData("05:00:00", "5 hours")]
        [InlineData("05:00:00:00", "5 days")]
        public void ApproximateStringShowsMostSignificantComponent(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:02:30", "2.5 minutes")]
        [InlineData("02:30:00", "2.5 hours")]
        [InlineData("02:12:00:00", "2.5 days")]
        public void ApproximateStringShowsDecimalForSmallerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:02:00", "2 minutes")]
        [InlineData("02:00:00", "2 hours")]
        [InlineData("02:00:00:00", "2 days")]
        public void ApproximateStringSkipsDecimalForSmallerIntegerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:00:07.777", "8 seconds")]
        [InlineData("00:06:12.234", "6 minutes")]
        [InlineData("05:58:32.012", "6 hours")]
        [InlineData("07:04:36:17.663", "7 days")]
        public void ApproximateStringRoundsForLargerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:00:01", "1 second")]
        [InlineData("00:01:00", "1 minute")]
        [InlineData("01:00:00", "1 hour")]
        public void ApproximateStringUsesSingularWhenAppropriate(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01:00:00:00", "24 hours")]
        [InlineData("01:12:00:00", "36 hours")]
        public void ApproximateStringShowsHoursUnder2Days(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToApproximateString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }
    }
}
