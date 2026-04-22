namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;

using System;

public class PersianDateTimeFormatTests
{
    [Test]
    public void First_Month_Is_Farvardin()
    {
        var s = PersianDateTimeFormatInfo.MonthNames[0];
        s.Should().Contain("فروردین");
    }

    [Test]
    public void Day_Name_Index_Has_Correct_Mapping()
    {
        PersianDateTimeFormatInfo.GetWeekDayByIndex(0).Should().Be("شنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(1).Should().Be("یکشنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(2).Should().Be("دوشنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(3).Should().Be("ﺳﻪشنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(4).Should().Be("چهارشنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(5).Should().Be("پنجشنبه");
        PersianDateTimeFormatInfo.GetWeekDayByIndex(6).Should().Be("جمعه");
    }

    [Test]
    public void Abbr_Day_Index_Has_Correct_Mapping()
    {
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(0).Should().Be("ش");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(1).Should().Be("ی");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(2).Should().Be("د");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(3).Should().Be("س");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(4).Should().Be("چ");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(5).Should().Be("پ");
        PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(6).Should().Be("ج");
    }

    [Test]
    public void Getting_Invalid_Day_Name_Index_Throws()
    {
        new Action(() => { PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(-1); }).Should().Throw<ArgumentOutOfRangeException>();
        new Action(() => { PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(7); }).Should().Throw<ArgumentOutOfRangeException>();
        new Action(() => { PersianDateTimeFormatInfo.GetWeekDayByIndex(-1); }).Should().Throw<ArgumentOutOfRangeException>();
        new Action(() => { PersianDateTimeFormatInfo.GetWeekDayByIndex(7); }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Day_Index_Has_Correct_Mapping()
    {
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Saturday).Should().Be(0);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Sunday).Should().Be(1);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Monday).Should().Be(2);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Tuesday).Should().Be(3);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Wednesday).Should().Be(4);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Thursday).Should().Be(5);
        PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Friday).Should().Be(6);
    }
}