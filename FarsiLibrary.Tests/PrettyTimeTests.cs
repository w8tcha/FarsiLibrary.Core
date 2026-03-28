namespace FarsiLibrary.Tests;

using System;
using System.Threading;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Formatter;
using FarsiLibrary.Tests.Helpers;

public class PrettyTimeTests
{
    [TestCase(-5, "4 days ago", "en-US") ]
    [TestCase(5, "5 days from now", "en-US")]
    [TestCase(5, "پنج روز بعد", "fa-IR")]
    [TestCase(-5, "چهار روز قبل", "fa-IR")]
    public void Can_Format_Days(int days, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddDays(days);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            result.Should().NotBeNull();
            result.Should().Be(expected);
        }
    }

    [TestCase(-5, "moments ago", "en-US")]
    [TestCase(5, "moments from now", "en-US")]
    [TestCase(5, "چند لحظه بعد", "fa-IR")]
    [TestCase(-5, "چند لحظه قبل", "fa-IR")]
    public void Can_Format_Seconds(int seconds, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var date = DateTime.Now.AddSeconds(seconds);
            var pretty = new PrettyTime();

            var result = pretty.Format(date);

            result.Should().NotBeNull();
            result.Should().Be(expected);
        }
    }

    [TestCase(-10, "9 minutes ago", "en-US")]
    [TestCase(10, "10 minutes from now", "en-US")]
    [TestCase(10, "ده دقیقه بعد", "fa-IR")]
    [TestCase(-10, "نه دقیقه قبل", "fa-IR")]
    public void Can_Format_Minutes(int minutes, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddMinutes(minutes);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            result.Should().NotBeNull();
            result.Should().Be(expected);
        }
    }

    [TestCase(-2, "1 hour ago", "en-US")]
    [TestCase(2, "2 hours from now", "en-US")]
    [TestCase(2, "دو ساعت بعد", "fa-IR")]
    [TestCase(-2, "يک ساعت قبل", "fa-IR")]
    public void Can_Format_Hours(int hours, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddHours(hours);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            result.Should().NotBeNull();
            result.Should().Be(expected);
        }
    }

    [TestCase(-1, "1 year ago", "en-US")]
    [TestCase(2, "2 years from now", "en-US")]
    [TestCase(50, "6 decades from now", "en-US")]
    [TestCase(100, "10 decades from now", "en-US")]
    [TestCase(101, "1 century from now", "en-US")]
    [TestCase(-1, "يک سال قبل", "fa-IR")]
    [TestCase(2, "دو سال بعد", "fa-IR")]
    [TestCase(50, "شش دهه بعد", "fa-IR")]
    [TestCase(100, "ده دهه بعد", "fa-IR")]
    [TestCase(101, "يک قرن بعد", "fa-IR")]
    public void Can_Format_Years(int years, string expected, string cultureName)
    {
        using (new CultureSwitchContext(new CultureInfo(cultureName)))
        {
            var datetime = DateTime.Now;
            var then = DateTime.Now.AddYears(years);
            var pretty = new PrettyTime(datetime);

            var result = pretty.Format(then);

            result.Should().NotBeNull();
            result.Should().Be(expected);
        }
    }

    [Test]
    public void Can_Compare_Two_Dates()
    {
        var from = new DateTime(2000, 1, 1);
        var to = new DateTime(2001, 1, 1);

        var duration = new PrettyTime(to).Format(from);

        duration.Should().Be("1 year ago");
    }

    [Test]
    public void Can_Compare_Two_Dates_Regardless_Of_Precedence()
    {
        var from = new DateTime(2000, 1, 1);
        var to = new DateTime(2001, 1, 1);

        var duration = new PrettyTime(from).Format(to);

        duration.Should().Be("1 year from now");
    }

    [Test]
    public void Default_Comparison_Compares_To_DateTimeNow()
    {
        var justNow = new PrettyTime().Format(DateTime.Now);

        justNow.Should().Be("moments from now");
    }

    [Test]
    public void Can_Convert_Dates_Using_ExtensionMethod()
    {
        var date = DateTime.Now;

        Thread.Sleep(1000); //to simulate delay

        var pretty = date.ToPrettyTime();

        pretty.Should().Be("moments ago");
    }
}