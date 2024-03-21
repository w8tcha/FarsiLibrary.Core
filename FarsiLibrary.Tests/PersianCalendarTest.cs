using PersianCalendar=FarsiLibrary.Core.Utils.PersianCalendar;

namespace FarsiLibrary.Tests;

using System;
using System.Linq;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Exceptions;

public class PersianCalendarTest
{
    private readonly PersianCalendar calendar = new();

    private readonly System.Globalization.PersianCalendar sysCalendar = new();

    [Fact]
    public void Can_Add_A_Month()
    {
        var pd = new PersianDate(1387, 12, 30);
        var dt1 = (DateTime)pd;
        var dt2 = this.calendar.AddMonths(dt1, 2);

        Assert.Equal(dt2.Month, dt1.Month + 2);
    }

    [Fact]
    public void Can_Add_A_Year()
    {
        var pd = PersianDate.Now;
        var dt1 = (DateTime)pd;
        var dt2 = this.calendar.AddYears(dt1, 2);

        Assert.Equal(dt2.Year, dt1.Year + 2);
    }

    [Fact]
    public void Can_Get_Day_Of_Month()
    {
        var pd = new PersianDate(1380, 1, 1);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfMonth(dt1);

        Assert.Equal(1, result);
    }

    [Fact]
    public void Can_Get_First_Day_Of_Year()
    {
        var pd = new PersianDate(1380, 1, 1);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfYear(dt1);

        Assert.Equal(1, result);
    }

    [Fact]
    public void Can_Get_Last_Day_Of_Year()
    {
        var pd = new PersianDate(1387, 12, 30);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfYear(dt1);

        Assert.Equal(366, result);
    }

    [Fact]
    public void Can_Get_Days_In_Last_Month()
    {
        var pd = new PersianDate(1387, 12, 1);
        var result = this.calendar.GetDaysInMonth(pd.Year, pd.Month);

        Assert.Equal(30, result);
    }

    [Fact]
    public void Can_Get_Days_In_Year()
    {
        var pd1 = new PersianDate(1387, 12, 30);
        var pd2 = new PersianDate(1386, 12, 29);

        var result1 = this.calendar.GetDaysInYear(pd1.Year);
        var result2 = this.calendar.GetDaysInYear(pd2.Year);

        Assert.Equal(366, result1);
        Assert.Equal(365, result2);
    }

    [Fact]
    public void Can_Get_Leap_Month()
    {
        var pd1 = new PersianDate(1387, 12, 30);
        var pd2 = new PersianDate(1386, 12, 29);

        var result1 = this.calendar.IsLeapMonth(pd1.Year, 12);
        var result2 = this.calendar.IsLeapMonth(pd2.Year, 12);

        Assert.True(result1);
        Assert.False(result2);
    }

    [Fact]
    public void Can_Get_Leap_Year()
    {
        const int Leap = 1387;
        const int Normal = 1386;

        var result1 = this.calendar.IsLeapYear(Leap);
        var result2 = this.calendar.IsLeapYear(Normal);

        Assert.True(result1);
        Assert.False(result2);
    }

    [Fact]
    public void ToDateTime_With_Invalid_Day_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.ToDateTime(1384, 1, 32, 0, 0, 0, 0));
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.ToDateTime(1384, 0, 0, 0, 0, 0, 0));
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.ToDateTime(1384, 1, 0, 0, 0, 0, 0));
    }

    [Fact]
    public void Can_Convert_ToDateTime_With_Leap_Year_Day()
    {
        var exception = Record.Exception(() => this.calendar.ToDateTime(1387, 12, 30, 0, 0, 0, 0));
        Assert.Null(exception);
    }

    [Fact]
    public void ToDateTime_With_Invalid_LeapYear_Day_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.ToDateTime(1388, 12, 30, 0, 0, 0, 0));
    }

    [Fact]
    public void Adding_Or_Removing_Month_Larger_Than_Max_Difference_Throws()
    {
        var dt = DateTime.Now;

        Assert.Throws<InvalidPersianDateException>(
            () => this.calendar.AddMonths(dt, PersianCalendar.MaxMonthDifference)); //Adding a large month value
        Assert.Throws<InvalidPersianDateException>(
            () => this.calendar.AddMonths(
                dt,
                PersianCalendar.MaxMonthDifference + 100)); //Adding a something greater than MaxMonthDifference
    }

    [Fact]
    public void Increamenting_Month_In_Leap_Year_Should_Move_To_Next_Year()
    {
        DateTime dt = new PersianDate(1386, 11, 30);

        var exception = Record.Exception(() => this.calendar.AddMonths(dt, 1));
        Assert.Null(exception);
    }

    [Fact]
    public void Increamenting_Year_In_Leap_Year_Should_Correct_The_Day_Value()
    {
        DateTime dt = new PersianDate(1387, 12, 30);
        PersianDate dtNext = this.calendar.AddYears(dt, 1);
        PersianDate dtPrevious = this.calendar.AddYears(dt, -1);

        Assert.Equal(29, dtNext.Day);
        Assert.Equal(29, dtPrevious.Day);
    }

    [Fact]
    public void Should_Use_Solar_Calendar()
    {
        Assert.Equal(CalendarAlgorithmType.SolarCalendar, this.calendar.AlgorithmType);
    }

    [Fact]
    public void Minimum_Supported_Date_Is_MinValue_Of_PersianDate()
    {
        Assert.Equal(PersianDate.MinValue, this.calendar.MinSupportedDateTime);
    }

    [Fact]
    public void Maximum_Supported_Date_Is_MaxValue_Of_PersianDate()
    {
        Assert.Equal(PersianDate.MaxValue, this.calendar.MaxSupportedDateTime);
    }

    [Fact]
    public void Always_Twelve_Months_In_Year()
    {
        PersianDate pdMin = PersianDate.MinValue;
        PersianDate pdMax = PersianDate.MaxValue;

        Assert.Equal(12, this.calendar.GetMonthsInYear(pdMax.Year));
        Assert.Equal(12, this.calendar.GetMonthsInYear(pdMin.Year));
    }

    [Fact]
    public void Era_Is_Always_PersianEra()
    {
        Assert.Equal(PersianCalendar.PersianEra, this.calendar.GetEra(PersianDate.MinValue));
        Assert.Equal(PersianCalendar.PersianEra, this.calendar.GetEra(PersianDate.MaxValue));
    }

    [Fact]
    public void Era_Returns_PersianEra()
    {
        Assert.Equal(PersianCalendar.PersianEra, this.calendar.Eras.FirstOrDefault());
    }

    [Fact]
    public void Can_Get_Number_Of_Leap_Years()
    {
        //335 leap years from the start of the calendar to 1387
        Assert.Equal(335, PersianCalendar.NumberOfLeapYearsUntil(1387));
    }

    [Fact]
    public void Can_Get_Correct_Month_From_Day_Value()
    {
        Assert.Equal(1, this.calendar.GetMonth(new PersianDate(1384, 1, 10)));
        Assert.Equal(2, this.calendar.GetMonth(new PersianDate(1384, 2, 22)));
        Assert.Equal(3, this.calendar.GetMonth(new PersianDate(1384, 3, 30)));
        Assert.Equal(4, this.calendar.GetMonth(new PersianDate(1384, 4, 1)));
        Assert.Equal(5, this.calendar.GetMonth(new PersianDate(1384, 5, 18)));
        Assert.Equal(6, this.calendar.GetMonth(new PersianDate(1384, 6, 2)));
        Assert.Equal(7, this.calendar.GetMonth(new PersianDate(1384, 7, 4)));
        Assert.Equal(8, this.calendar.GetMonth(new PersianDate(1384, 8, 12)));
        Assert.Equal(9, this.calendar.GetMonth(new PersianDate(1384, 9, 13)));
        Assert.Equal(10, this.calendar.GetMonth(new PersianDate(1384, 10, 1)));
        Assert.Equal(11, this.calendar.GetMonth(new PersianDate(1384, 11, 8)));
        Assert.Equal(12, this.calendar.GetMonth(new PersianDate(1384, 12, 5)));
    }

    [Fact]
    public void To_Four_Year_Digit_With_Large_Year_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.ToFourDigitYear(9999));
    }

    [Fact]
    public void To_Four_Year_Digit_Only_Converts_Two_Digit_Values()
    {
        Assert.Equal(100, this.calendar.ToFourDigitYear(100));
    }

    [Fact]
    public void Setting_Two_Digit_Year_Over_Limits_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => this.calendar.TwoDigitYearMax = 10);
        Assert.Throws<InvalidOperationException>(() => this.calendar.TwoDigitYearMax = 9999);
    }

    [Fact]
    public void Can_Convert_To_TwoDigit_Year()
    {
        this.calendar.TwoDigitYearMax = 1400;
        Assert.Equal(1387, this.calendar.ToFourDigitYear(87));

        this.calendar.TwoDigitYearMax = 1500;
        Assert.Equal(1488, this.calendar.ToFourDigitYear(88));
    }

    [Fact]
    public void Can_Set_TwoDigit_Year_Value()
    {
        var exception = Record.Exception(() => this.calendar.TwoDigitYearMax = 1500);
        Assert.Null(exception);
        Assert.Equal(1500, this.calendar.TwoDigitYearMax);
    }

    [Fact]
    public void Can_Get_DayOfWeek()
    {
        var dt = new DateTime(2008, 10, 21);
        Assert.Equal(DayOfWeek.Tuesday, this.calendar.GetDayOfWeek(dt));
    }

    [Fact]
    public void Can_Get_Century()
    {
        var dt = new DateTime(2008, 10, 21); // Should be 14th Century
        Assert.Equal(14, PersianCalendar.GetCentury(dt));
    }

    [Fact]
    public void Can_Convert_From_Gregorian_Leap_Year()
    {
        var dt = new DateTime(2008, 10, 9);
        _ = new DateTime(1387, 7, 18, 0, 0, 0, this.sysCalendar);
        var pd = dt.ToPersianDate();

        Assert.Equal(DayOfWeek.Thursday, pd.DayOfWeek);
    }

    [Fact]
    public void Getting_Invalid_Era_Will_Throw()
    {
        /* only supports PersianEra = 1 */
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.GetMonthsInYear(2000, -1));
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.GetMonthsInYear(2000, 2));
    }

    [Fact]
    public void Getting_Era_With_Invalid_Date_Range_Will_Throw()
    {
        var dt = new DateTime(000000000000000000L, DateTimeKind.Local);
        Assert.Throws<InvalidPersianDateException>(() => this.calendar.GetEra(dt));
    }

    [Fact]
    public void Can_()
    {
        var dt = DateTime.MaxValue;

        Assert.Throws<InvalidPersianDateException>(
            () => this.calendar.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond));
    }
}