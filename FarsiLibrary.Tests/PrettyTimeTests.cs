namespace FarsiLibrary.Tests;

using System;
using System.Threading;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Formatter;
using FarsiLibrary.Tests.Helpers;

using Xunit;

public class PrettyTimeTests
{
    [Theory]
    [InlineData(-5, "4 days ago", "en-US") ]
    [InlineData(5, "5 days from now", "en-US")]
    [InlineData(5, "پنج روز بعد", "fa-IR")]
    [InlineData(-5, "چهار روز قبل", "fa-IR")] 
    public void Can_Format_Days(int days, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddDays(days);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(-5, "moments ago", "en-US")]
    [InlineData(5, "moments from now", "en-US")]
    [InlineData(5, "چند لحظه بعد", "fa-IR")]
    [InlineData(-5, "چند لحظه قبل", "fa-IR")]
    public void Can_Format_Seconds(int seconds, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var date = DateTime.Now.AddSeconds(seconds);
            var pretty = new PrettyTime();

            var result = pretty.Format(date);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(-10, "9 minutes ago", "en-US")]
    [InlineData(10, "10 minutes from now", "en-US")]
    [InlineData(10, "ده دقیقه بعد", "fa-IR")]
    [InlineData(-10, "نه دقیقه قبل", "fa-IR")]
    public void Can_Format_Minutes(int minutes, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddMinutes(minutes);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(-2, "1 hour ago", "en-US")]
    [InlineData(2, "2 hours from now", "en-US")]
    [InlineData(2, "دو ساعت بعد", "fa-IR")]
    [InlineData(-2, "يک ساعت قبل", "fa-IR")]
    public void Can_Format_Hours(int hours, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddHours(hours);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }

    [Theory]
    [InlineData(-1, "1 year ago", "en-US")]
    [InlineData(2, "2 years from now", "en-US")]
    [InlineData(50, "5 decades from now", "en-US")]
    //[InlineData(100, "10 decades from now", "en-US")]
    [InlineData(101, "1 century from now", "en-US")]
    [InlineData(-1, "يک سال قبل", "fa-IR")]
    [InlineData(2, "دو سال بعد", "fa-IR")]
    [InlineData(50, "پنج دهه بعد", "fa-IR")]
    //[InlineData(100, "نه دهه بعد", "fa-IR")]
    [InlineData(101, "يک قرن بعد", "fa-IR")]
    public void Can_Format_Years(int years, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddYears(years);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public void Can_Compare_Two_Dates()
    {
        var from = new DateTime(2000, 1, 1);
        var to = new DateTime(2001, 1, 1);

        var duration = new PrettyTime(to).Format(from);

        Assert.Equal("1 year ago", duration);
    }

    [Fact]
    public void Can_Compare_Two_Dates_Regardless_Of_Precedence()
    {
        var from = new DateTime(2000, 1, 1);
        var to = new DateTime(2001, 1, 1);

        var duration = new PrettyTime(from).Format(to);

        Assert.Equal("1 year from now", duration);
    }

    [Fact]
    public void Default_Comparison_Compares_To_DateTimeNow()
    {
        var justNow = new PrettyTime().Format(DateTime.Now);

        Assert.Equal("moments from now", justNow);
    }

    [Fact]
    public void Can_Convert_Dates_Using_ExtensionMethod()
    {
        var date = DateTime.Now;
            
        Thread.Sleep(1000); //to simulate delay

        var pretty = date.ToPrettyTime();

        Assert.Equal("moments ago", pretty);
    }
}