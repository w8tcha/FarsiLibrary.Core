namespace FarsiLibrary.Tests;

using System;

public class PersianDateTimeFormatTests
{
    [Fact]
    public void First_Month_Is_Farvardin()
    {
        var s = PersianDateTimeFormatInfo.MonthNames[0];
        Assert.Contains("فروردین", s);
    }

    [Fact]
    public void Day_Name_Index_Has_Correct_Mapping()
    {
        Assert.Equal("شنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(0));
        Assert.Equal("یکشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(1));
        Assert.Equal("دوشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(2));
        Assert.Equal("ﺳﻪشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(3));
        Assert.Equal("چهارشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(4));
        Assert.Equal("پنجشنبه", PersianDateTimeFormatInfo.GetWeekDayByIndex(5));
        Assert.Equal("جمعه", PersianDateTimeFormatInfo.GetWeekDayByIndex(6));
    }

    [Fact]
    public void Abbr_Day_Index_Has_Correct_Mapping()
    {
        Assert.Equal("ش", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(0));
        Assert.Equal("ی", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(1));
        Assert.Equal("د", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(2));
        Assert.Equal("س", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(3));
        Assert.Equal("چ", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(4));
        Assert.Equal("پ", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(5));
        Assert.Equal("ج", PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(6));            
    }

    [Fact]
    public void Getting_Invalid_Day_Name_Index_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayAbbrByIndex(7));
        Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayByIndex(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => PersianDateTimeFormatInfo.GetWeekDayByIndex(7));
    }

    [Fact]
    public void Day_Index_Has_Correct_Mapping()
    {
        Assert.Equal(0, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Saturday));
        Assert.Equal(1, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Sunday));
        Assert.Equal(2, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Monday));
        Assert.Equal(3, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Tuesday));
        Assert.Equal(4, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Wednesday));
        Assert.Equal(5, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Thursday));
        Assert.Equal(6, PersianDateTimeFormatInfo.GetDayIndex(DayOfWeek.Friday));
    }
}