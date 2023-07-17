namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;

public class PersianDateConverterTests
{
    [Fact]
    public void Can_Get_DayOfWeek_From_PersianDate_Instance()
    {
        var pd = new PersianDate(1387, 7, 7); //7 Mehr equals Doshanbeh
        var weekDay = PersianDateConverter.DayOfWeek(pd);

        Assert.Equal(PersianWeekDayNames.Default.Yekshanbeh, weekDay);
    }

    [Fact]
    public void Can_Get_DayOfWeek_From_DateTime_Instance()
    {
        var dt = new DateTime(2008, 10, 21); //October 30th, equals Seshanbeh
        var weekday = PersianDateConverter.DayOfWeek(dt);

        Assert.Equal(PersianWeekDayNames.Default.Seshanbeh, weekday);
    }

    [Fact]
    public void Converting_Out_Of_Range_Dates_Will_ReturnEmpty()
    {
        var pd = (PersianDate)PersianDate.MinValue;
        var weekday = PersianDateConverter.DayOfWeek(pd);

        Assert.Equal(string.Empty, weekday);
    }

    [Fact]
    public void Day_Of_Week_Has_Correct_Mapping()
    {
        var dt = new DateTime(2008, 10, 1);
        var weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Chaharshanbeh, weekday);

        dt = new DateTime(2008, 10, 2);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Panjshanbeh, weekday);

        dt = new DateTime(2008, 10, 3);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Jomeh, weekday);

        dt = new DateTime(2008, 10, 4);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Shanbeh, weekday);

        dt = new DateTime(2008, 10, 5);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Yekshanbeh, weekday);

        dt = new DateTime(2008, 10, 6);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Doshanbeh, weekday);

        dt = new DateTime(2008, 10, 7);
        weekday = PersianDateConverter.DayOfWeek(dt);
        Assert.Equal(PersianWeekDayNames.Default.Seshanbeh, weekday);
    }

    [Fact]
    public void Can_Get_Correct_Number_Of_Months()
    {
        var pd = new PersianDate(1380, 7, 1);
        Assert.Equal(30, pd.MonthDays);
    }

    [Fact]
    public void Can_Convert_Strings_ToDateTime()
    {
        var pd = new PersianDate(1380, 1, 1);
        var converted = pd.ToDateTime();
        var dt = PersianDateConverter.ToGregorianDateTime(pd.ToString());

        Assert.Equal(dt, converted);
    }

    [Fact]
    public void Can_Convert_LeapYears_Correctly()
    {
        var dt = new DateTime(2009, 3, 20); //Converts to a leap year in Persian Date (30th Esfand 1387)
        var pd = PersianDateConverter.ToPersianDate(dt);

        Assert.Equal(12, pd.Month);
        Assert.Equal(30, pd.Day);
        Assert.Equal(30, pd.MonthDays);
    }

    [Fact]
    public void Can_Convert_Leap_PersianDate_To_DateTime_String()
    {
        var dt = new DateTime(2009, 3, 20);
        var pd = dt.ToPersianDate();
        var date = PersianDateConverter.ToGregorianDate(pd);

        Assert.Equal(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture), date);
    }

    [Fact]
    public void Can_Convert_Normal_PersianDate_To_DateTime_String()
    {
        var dt = new DateTime(2008, 10, 20); // 29th Mehr 1387
        var pd = dt.ToPersianDate();
        var date = PersianDateConverter.ToGregorianDate(pd);

        Assert.Equal(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture), date);
    }

    [Fact]
    public void Can_Convert_With_Time_Part()
    {
        var dt = new DateTime(2000, 1, 1); //1378/10/11
        var pd = PersianDateConverter.ToPersianDate(dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), new TimeSpan(11, 45, 26));

        Assert.Equal(11, pd.Hour);
        Assert.Equal(45, pd.Minute);
        Assert.Equal(26, pd.Second);
        Assert.Equal(1378, pd.Year);
        Assert.Equal(10, pd.Month);
        Assert.Equal(11, pd.Day);
    }

    [Fact]
    public void Can_Convert_Directly_To_Gregorian_Date_From_String()
    {
        var dt = PersianDateConverter.ToGregorianDateTime("1387/07/29 02:31:30"); //2008/10/20

        Assert.Equal(2008, dt.Year);
        Assert.Equal(10,dt.Month);
        Assert.Equal(20,dt.Day);
        Assert.Equal(2, dt.Hour);
        Assert.Equal(31,dt.Minute);
        Assert.Equal(30, dt.Second);
    }
}