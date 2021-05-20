using System;
using System.Globalization;

using Vivelin.Web.Home;

using Xunit;

namespace Vivelin.Web.Tests
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("00:00:05", "5 seconds ago")]
        [InlineData("00:05:00", "5 minutes ago")]
        [InlineData("05:00:00", "5 hours ago")]
        [InlineData("05:00:00:00", "5 days ago")]
        [InlineData("-05:00:00:00", "in 5 days")]
        public void RelativeStringShowsMostSignificantComponent(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:02:30", "2.5 minutes ago")]
        [InlineData("02:30:00", "2.5 hours ago")]
        [InlineData("02:12:00:00", "2.5 days ago")]
        [InlineData("-02:12:00:00", "in 2.5 days")]
        public void RelativeStringShowsDecimalForSmallerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:02:00", "2 minutes ago")]
        [InlineData("02:00:00", "2 hours ago")]
        [InlineData("02:00:00:00", "2 days ago")]
        [InlineData("-02:00:00:00", "in 2 days")]
        public void RelativeStringSkipsDecimalForSmallerIntegerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:00:07.777", "8 seconds ago")]
        [InlineData("00:06:12.234", "6 minutes ago")]
        [InlineData("05:58:32.012", "6 hours ago")]
        [InlineData("07:04:36:17.663", "7 days ago")]
        [InlineData("-07:04:36:17.663", "in 7 days")]
        public void RelativeStringRoundsForLargerValues(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("00:00:01", "1 second ago")]
        [InlineData("00:01:00", "1 minute ago")]
        [InlineData("01:00:00", "1 hour ago")]
        [InlineData("-01:00:00", "in 1 hour")]
        public void RelativeStringUsesSingularWhenAppropriate(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01:00:00:00", "24 hours ago")]
        [InlineData("01:12:00:00", "36 hours ago")]
        [InlineData("-01:00:00:00", "in 24 hours")]
        public void RelativeStringShowsHoursUnder2Days(string timeSpan, string expected)
        {
            var value = TimeSpan.Parse(timeSpan, CultureInfo.InvariantCulture);
            var result = value.ToRelativeString(CultureInfo.InvariantCulture);
            Assert.Equal(expected, result);
        }
    }
}
