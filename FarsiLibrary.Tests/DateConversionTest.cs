namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;

using PersianCalendar = FarsiLibrary.Core.Utils.PersianCalendar;

public class DateConversionTest
{
    private readonly PersianCalendar calendar = new();

    [Test]
    public void DateTime_Automatically_Cast_To_PersianDate_Equality()
    {
        var pd = new PersianDate(1403, 12, 30);
        PersianDate dt = new DateTime(2025, 3, 20);

        pd.Year.Should().Be(dt.Year);
        pd.Month.Should().Be(dt.Month);
        pd.Day.Should().Be(dt.Day);
    }

    [Test]
    public void Check_Equality_After_Conversion()
    {
        var dt = new DateTime(2025, 3, 20);
        PersianDate pd1 = new DateTime(2025, 3, 20);
        PersianDate pd2 = dt;

        pd2.Year.Should().Be(pd1.Year);
        pd2.Month.Should().Be(pd1.Month);
        pd2.Day.Should().Be(pd1.Day);
         pd2.Should().Be(pd1);
        (pd1 == pd2).Should().BeTrue();
    }

    [Test]
    public void Leap_Years_Work_When_Converted()
    {
        var dt1 = new DateTime(2025, 3, 20);
        PersianDate pd1 = dt1;

        this.calendar.IsLeapYear(pd1.Year).Should().BeTrue("Should be a leap year.");
        this.calendar.IsLeapYear(pd1.Year, PersianCalendar.PersianEra).Should().BeTrue();

        var dt2 = new DateTime(2026, 3, 20);
        PersianDate pd2 = dt2;

        this.calendar.IsLeapYear(pd2.Year).Should().BeFalse("Should not be a leap year.");
        this.calendar.IsLeapYear(pd2.Year, PersianCalendar.PersianEra).Should().BeFalse();
    }

    [Test]
    public void Normal_Years_Work_When_Converted()
    {
        var pd = new PersianDate(1387, 1, 1);
        this.calendar.IsLeapYear(pd.Year).Should().BeTrue();
    }
}