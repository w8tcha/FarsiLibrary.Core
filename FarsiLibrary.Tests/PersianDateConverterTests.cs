namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;

public class PersianDateConverterTests
{
    [Test]
    public void Can_Get_DayOfWeek_From_PersianDate_Instance()
    {
        var pd = new PersianDate(1387, 7, 7); //7 Mehr equals Doshanbeh
        var weekDay = PersianDateConverter.DayOfWeek(pd);

        weekDay.Should().Be(PersianWeekDayNames.Default.Yekshanbeh);
    }

    [Test]
    public void Can_Get_DayOfWeek_From_DateTime_Instance()
    {
        var dt = new DateTime(2008, 10, 21); //October 30th, equals Seshanbeh
        var weekday = PersianDateConverter.DayOfWeek(dt);

        weekday.Should().Be(PersianWeekDayNames.Default.Seshanbeh);
    }

    [Test]
    public void Converting_Out_Of_Range_Dates_Will_ReturnEmpty()
    {
        var pd = (PersianDate)PersianDate.MinValue;
        var weekday = PersianDateConverter.DayOfWeek(pd);

        weekday.Should().Be(string.Empty);
    }

    [Test]
    public void Day_Of_Week_Has_Correct_Mapping()
    {
        var dt = new DateTime(2008, 10, 1);
        var weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Chaharshanbeh);

        dt = new DateTime(2008, 10, 2);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Panjshanbeh);

        dt = new DateTime(2008, 10, 3);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Jomeh);

        dt = new DateTime(2008, 10, 4);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Shanbeh);

        dt = new DateTime(2008, 10, 5);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Yekshanbeh);

        dt = new DateTime(2008, 10, 6);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Doshanbeh);

        dt = new DateTime(2008, 10, 7);
        weekday = PersianDateConverter.DayOfWeek(dt);
        weekday.Should().Be(PersianWeekDayNames.Default.Seshanbeh);
    }

    [Test]
    public void Can_Get_Correct_Number_Of_Months()
    {
        var pd = new PersianDate(1380, 7, 1);
        pd.MonthDays.Should().Be(30);
    }

    [Test]
    public void Can_Convert_Strings_ToDateTime()
    {
        var pd = new PersianDate(1380, 1, 1);
        var converted = pd.ToDateTime();
        var dt = PersianDateConverter.ToGregorianDateTime(pd.ToString());

        converted.Should().Be(dt);
    }

    [Test]
    public void Can_Convert_LeapYears_Correctly()
    {
        var dt = new DateTime(2009, 3, 20); //Converts to a leap year in Persian Date (30th Esfand 1387)
        var pd = PersianDateConverter.ToPersianDate(dt);

        pd.Month.Should().Be(12);
        pd.Day.Should().Be(30);
        pd.MonthDays.Should().Be(30);
    }

    [Test]
    public void Can_Convert_Leap_PersianDate_To_DateTime_String()
    {
        var dt = new DateTime(2009, 3, 20);
        var pd = dt.ToPersianDate();
        var date = PersianDateConverter.ToGregorianDate(pd);

        date.Should().Be(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
    }

    [Test]
    public void Can_Convert_Normal_PersianDate_To_DateTime_String()
    {
        var dt = new DateTime(2008, 10, 20); // 29th Mehr 1387
        var pd = dt.ToPersianDate();
        var date = PersianDateConverter.ToGregorianDate(pd);

        date.Should().Be(dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
    }

    [Test]
    public void Can_Convert_With_Time_Part()
    {
        var dt = new DateTime(2000, 1, 1); //1378/10/11
        var pd = PersianDateConverter.ToPersianDate(dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture), new TimeSpan(11, 45, 26));

        pd.Hour.Should().Be(11);
        pd.Minute.Should().Be(45);
        pd.Second.Should().Be(26);
        pd.Year.Should().Be(1378);
        pd.Month.Should().Be(10);
        pd.Day.Should().Be(11);
    }

    [Test]
    public void Can_Convert_Directly_To_Gregorian_Date_From_String()
    {
        var dt = PersianDateConverter.ToGregorianDateTime("1387/07/29 02:31:30"); //2008/10/20

        dt.Year.Should().Be(2008);
        dt.Month.Should().Be(10);
        dt.Day.Should().Be(20);
        dt.Hour.Should().Be(2);
        dt.Minute.Should().Be(31);
        dt.Second.Should().Be(30);
    }
}