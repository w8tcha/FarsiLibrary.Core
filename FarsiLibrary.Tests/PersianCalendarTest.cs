namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Exceptions;

using System;
using System.Linq;

public class PersianCalendarTest
{
    private readonly PersianCalendar calendar = new();

    private readonly System.Globalization.PersianCalendar sysCalendar = new();

    [Test]
    public void Can_Add_A_Month()
    {
        var pd = new PersianDate(1387, 12, 30);
        var dt1 = (DateTime)pd;
        var dt2 = this.calendar.AddMonths(dt1, 2);

         (dt1.Month + 2).Should().Be(dt2.Month);
    }

    [Test]
    public void Can_Add_A_Year()
    {
        var pd = PersianDate.Now;
        var dt1 = (DateTime)pd;
        var dt2 = this.calendar.AddYears(dt1, 2);

         (dt1.Year + 2).Should().Be(dt2.Year);
    }

    [Test]
    public void Can_Get_Day_Of_Month()
    {
        var pd = new PersianDate(1380, 1, 1);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfMonth(dt1);

         result.Should().Be(1);
    }

    [Test]
    public void Can_Get_First_Day_Of_Year()
    {
        var pd = new PersianDate(1380, 1, 1);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfYear(dt1);

         result.Should().Be(1);
    }

    [Test]
    public void Can_Get_Last_Day_Of_Year()
    {
        var pd = new PersianDate(1387, 12, 30);
        var dt1 = (DateTime)pd;
        var result = this.calendar.GetDayOfYear(dt1);

         result.Should().Be(366);
    }

    [Test]
    public void Can_Get_Days_In_Last_Month()
    {
        var pd = new PersianDate(1387, 12, 1);
        var result = this.calendar.GetDaysInMonth(pd.Year, pd.Month);

         result.Should().Be(30);
    }

    [Test]
    public void Can_Get_Days_In_Year()
    {
        var pd1 = new PersianDate(1387, 12, 30);
        var pd2 = new PersianDate(1386, 12, 29);

        var result1 = this.calendar.GetDaysInYear(pd1.Year);
        var result2 = this.calendar.GetDaysInYear(pd2.Year);

         result1.Should().Be(366);
         result2.Should().Be(365);
    }

    [Test]
    public void Can_Get_Leap_Month()
    {
        var pd1 = new PersianDate(1387, 12, 30);
        var pd2 = new PersianDate(1386, 12, 29);

        var result1 = this.calendar.IsLeapMonth(pd1.Year, 12);
        var result2 = this.calendar.IsLeapMonth(pd2.Year, 12);

        result1.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Test]
    public void Can_Get_Leap_Year()
    {
        const int leap = 1387;
        const int normal = 1386;

        var result1 = this.calendar.IsLeapYear(leap);
        var result2 = this.calendar.IsLeapYear(normal);

        result1.Should().BeTrue();
        result2.Should().BeFalse();
    }

    [Test]
    public void ToDateTime_With_Invalid_Day_Throws()
    {
        new Action(() => { this.calendar.ToDateTime(1384, 1, 32, 0, 0, 0, 0); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { this.calendar.ToDateTime(1384, 0, 0, 0, 0, 0, 0); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { this.calendar.ToDateTime(1384, 1, 0, 0, 0, 0, 0); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Can_Convert_ToDateTime_With_Leap_Year_Day()
    {
        var exception = () => { this.calendar.ToDateTime(1387, 12, 30, 0, 0, 0, 0); };
        exception.Should().NotThrow();
    }

    [Test]
    public void ToDateTime_With_Invalid_LeapYear_Day_Throws()
    {
        new Action(() => { this.calendar.ToDateTime(1388, 12, 30, 0, 0, 0, 0); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Adding_Or_Removing_Month_Larger_Than_Max_Difference_Throws()
    {
        var dt = DateTime.Now;

        new Action(() => { this.calendar.AddMonths(dt, PersianCalendar.MaxMonthDifference); }).Should().Throw<InvalidPersianDateException>(); //Adding a large month value
        new Action(() =>
        {
            this.calendar.AddMonths(
                dt,
                PersianCalendar.MaxMonthDifference + 100);
        }).Should().Throw<InvalidPersianDateException>(
        ); //Adding a something greater than MaxMonthDifference
    }

    [Test]
    public void Incrementing_Month_In_Leap_Year_Should_Move_To_Next_Year()
    {
        DateTime dt = new PersianDate(1386, 11, 30);

        var exception = () => { this.calendar.AddMonths(dt, 1); };
        exception.Should().NotThrow();
    }

    [Test]
    public void Incrementing_Year_In_Leap_Year_Should_Correct_The_Day_Value()
    {
        DateTime dt = new PersianDate(1387, 12, 30);
        PersianDate dtNext = this.calendar.AddYears(dt, 1);
        PersianDate dtPrevious = this.calendar.AddYears(dt, -1);

         dtNext.Day.Should().Be(29);
         dtPrevious.Day.Should().Be(29);
    }

    [Test]
    public void Should_Use_Solar_Calendar()
    {
         this.calendar.AlgorithmType.Should().Be(CalendarAlgorithmType.SolarCalendar);
    }

    [Test]
    public void Minimum_Supported_Date_Is_MinValue_Of_PersianDate()
    {
         this.calendar.MinSupportedDateTime.Should().Be(PersianDate.MinValue);
    }

    [Test]
    public void Maximum_Supported_Date_Is_MaxValue_Of_PersianDate()
    {
         this.calendar.MaxSupportedDateTime.Should().Be(PersianDate.MaxValue);
    }

    [Test]
    public void Always_Twelve_Months_In_Year()
    {
        PersianDate pdMin = PersianDate.MinValue;
        PersianDate pdMax = PersianDate.MaxValue;

         this.calendar.GetMonthsInYear(pdMax.Year).Should().Be(12);
         this.calendar.GetMonthsInYear(pdMin.Year).Should().Be(12);
    }

    [Test]
    public void Era_Is_Always_PersianEra()
    {
         this.calendar.GetEra(PersianDate.MinValue).Should().Be(PersianCalendar.PersianEra);
         this.calendar.GetEra(PersianDate.MaxValue).Should().Be(PersianCalendar.PersianEra);
    }

    [Test]
    public void Era_Returns_PersianEra()
    {
         this.calendar.Eras.FirstOrDefault().Should().Be(PersianCalendar.PersianEra);
    }

    [Test]
    public void Can_Get_Number_Of_Leap_Years()
    {
        //335 leap years from the start of the calendar to 1387
         PersianCalendar.NumberOfLeapYearsUntil(1387).Should().Be(335);
    }

    [Test]
    public void Can_Get_Correct_Month_From_Day_Value()
    {
        this.calendar.GetMonth(new PersianDate(1384, 1, 10)).Should().Be(1);
        this.calendar.GetMonth(new PersianDate(1384, 2, 22)).Should().Be(2);
        this.calendar.GetMonth(new PersianDate(1384, 3, 30)).Should().Be(3);
        this.calendar.GetMonth(new PersianDate(1384, 4, 1)).Should().Be(4);
        this.calendar.GetMonth(new PersianDate(1384, 5, 18)).Should().Be(5);
        this.calendar.GetMonth(new PersianDate(1384, 6, 2)).Should().Be(6);
        this.calendar.GetMonth(new PersianDate(1384, 7, 4)).Should().Be(7);
        this.calendar.GetMonth(new PersianDate(1384, 8, 12)).Should().Be(8);
        this.calendar.GetMonth(new PersianDate(1384, 9, 13)).Should().Be(9);
        this.calendar.GetMonth(new PersianDate(1384, 10, 1)).Should().Be(10);
        this.calendar.GetMonth(new PersianDate(1384, 11, 8)).Should().Be(11);
        this.calendar.GetMonth(new PersianDate(1384, 12, 5)).Should().Be(12);
    }

    [Test]
    public void To_Four_Year_Digit_With_Large_Year_Throws()
    {
        new Action(() => { _ = this.calendar.ToFourDigitYear(9999); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void To_Four_Year_Digit_Only_Converts_Two_Digit_Values()
    {
         this.calendar.ToFourDigitYear(100).Should().Be(100);
    }

    [Test]
    public void Setting_Two_Digit_Year_Over_Limits_Throws()
    {
        new Action(() => { this.calendar.TwoDigitYearMax = 10; }).Should().Throw<InvalidOperationException>();
        new Action(() => { this.calendar.TwoDigitYearMax = 9999; }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Can_Convert_To_TwoDigit_Year()
    {
        this.calendar.TwoDigitYearMax = 1400;
         this.calendar.ToFourDigitYear(87).Should().Be(1387);

        this.calendar.TwoDigitYearMax = 1500;
         this.calendar.ToFourDigitYear(88).Should().Be(1488);
    }

    [Test]
    public void Can_Set_TwoDigit_Year_Value()
    {
        var exception = () => { this.calendar.TwoDigitYearMax = 1500; };
        exception.Should().NotThrow();
        this.calendar.TwoDigitYearMax.Should().Be(1500);
    }

    [Test]
    public void Can_Get_DayOfWeek()
    {
        var dt = new DateTime(2008, 10, 21);
         this.calendar.GetDayOfWeek(dt).Should().Be(DayOfWeek.Tuesday);
    }

    [Test]
    public void Can_Get_Century()
    {
        var dt = new DateTime(2008, 10, 21); // Should be 14th Century
         PersianCalendar.GetCentury(dt).Should().Be(14);
    }

    [Test]
    public void Can_Convert_From_Gregorian_Leap_Year()
    {
        var dt = new DateTime(2008, 10, 9);
        _ = new DateTime(1387, 7, 18, 0, 0, 0, this.sysCalendar);
        var pd = dt.ToPersianDate();

         pd.DayOfWeek.Should().Be(DayOfWeek.Thursday);
    }

    [Test]
    public void Getting_Invalid_Era_Will_Throw()
    {
        /* only supports PersianEra = 1 */
        new Action(() => { _ = this.calendar.GetMonthsInYear(2000, -1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = this.calendar.GetMonthsInYear(2000, 2); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Getting_Era_With_Invalid_Date_Range_Will_Throw()
    {
        var dt = new DateTime(000000000000000000L, DateTimeKind.Local);
        new Action(() => { _ = this.calendar.GetEra(dt); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Can_()
    {
        var dt = DateTime.MaxValue;

        new Action(() =>
        {
            this.calendar.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }).Should().Throw<InvalidPersianDateException>();
    }
}