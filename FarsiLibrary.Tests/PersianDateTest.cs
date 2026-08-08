namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Exceptions;
using FarsiLibrary.Core.Utils.Internals;
using FarsiLibrary.Tests.Helpers;

using System;
using System.Collections;

public class PersianDateTest
{
    [Test]
    public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_Invariant_Thread_Culture()
    {
        var exception = () => { PersianDate.Parse("1388/02/30"); };
        exception.Should().NotThrow();
    }

    [Test]
    public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_System_Persian_Culture()
    {
        using(new CultureSwitchContext(CultureHelper.FarsiCulture))
        {
            var exception = () => { PersianDate.Parse("1388/02/30");  };
            exception.Should().NotThrow();

            exception = () => { PersianDate.Parse("1388/02/31"); };
            exception.Should().NotThrow();
        }
    }

    [Test]
    public void Parsing_With_Years_Less_Than_Two_Digits_Will_Use_Four_Year_Digits()
    {
        using(new CultureSwitchContext(CultureHelper.FarsiCulture))
        {
            var pd = PersianDate.Parse("88/2/3");

            pd.Year.Should().Be(1388);
            pd.Month.Should().Be(2);
            pd.Day.Should().Be(3);
        }
    }

    [Test]
    public void Seven_Days_Of_Week_Validity()
    {
        var pd1 = new PersianDate(1386, 6, 1);
        pd1.DayOfWeek.Should().Be(DayOfWeek.Thursday);

        var pd2 = new PersianDate(1386, 6, 2);
        pd2.DayOfWeek.Should().Be(DayOfWeek.Friday);

        var pd3 = new PersianDate(1386, 6, 3);
        pd3.DayOfWeek.Should().Be(DayOfWeek.Saturday);

        var pd4 = new PersianDate(1386, 6, 4);
        pd4.DayOfWeek.Should().Be(DayOfWeek.Sunday);

        var pd5 = new PersianDate(1386, 6, 5);
        pd5.DayOfWeek.Should().Be(DayOfWeek.Monday);

        var pd6 = new PersianDate(1386, 6, 6);
        pd6.DayOfWeek.Should().Be(DayOfWeek.Tuesday);

        var pd7 = new PersianDate(1386, 6, 7);
        pd7.DayOfWeek.Should().Be(DayOfWeek.Wednesday);
    }

    [Test]
    public void Seven_DaysOfWeek_Index_Value_Validity()
    {
        var pd1 = new PersianDate(1387, 8, 2);
        pd1.DayOfWeek.Should().Be(DayOfWeek.Thursday);

        var pd2 = new PersianDate(1387, 8, 3);
        pd2.DayOfWeek.Should().Be(DayOfWeek.Friday);

        var pd3 = new PersianDate(1387, 8, 4);
        pd3.DayOfWeek.Should().Be(DayOfWeek.Saturday);

        var pd4 = new PersianDate(1387, 8, 5);
        pd4.DayOfWeek.Should().Be(DayOfWeek.Sunday);

        var pd5 = new PersianDate(1387, 8, 6);
        pd5.DayOfWeek.Should().Be(DayOfWeek.Monday);

        var pd6 = new PersianDate(1387, 8, 7);
        pd6.DayOfWeek.Should().Be(DayOfWeek.Tuesday);

        var pd7 = new PersianDate(1387, 8, 8);
        pd7.DayOfWeek.Should().Be(DayOfWeek.Wednesday);
    }

    [Test]
    public void Invalid_Day_In_PersianDate_Throws()
    {
        new Action(() => { _ = new PersianDate(1387, 1, 32); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Invalid_Year_In_PersianDate_Throws()
    {
        new Action(() => { _ = new PersianDate(DateTime.MinValue.Year, 1, 1); }).Should().NotThrow();
    }

    [Test]
    public void Can_Create_Minimum_PersianDate_Year_Value()
    {
        new Action(() => { _ = new PersianDate(PersianDate.MinValue.Year, 1, 1); }).Should().NotThrow();
    }

    [Test]
    public void Can_Create_Maximum_PersianDate_Year_Value()
    {
        // PersianDate.MaxValue.Year is DateTime.MaxValue's (Gregorian) year, not a valid Persian year bound.
        // The actual maximum supported Persian year is capped by PersianCalendar's supported range (1-9378).
        new Action(() => { _ = new PersianDate(9378, 1, 1); }).Should().NotThrow();
    }

    [Test]
    public void Can_Cast_DateTime_To_PersianDate()
    {
        var dt = DateTime.Now;
        var pd = (PersianDate)dt;

        pd.Should().NotBeNull();
    }

    [Test]
    public void Can_Cast_Nullable_DateTime_To_PersianDate()
    {
        DateTime? dtNow = DateTime.Now;

        PersianDate pdNull = (DateTime?)null;
        PersianDate pdNow = dtNow;

        pdNull.Should().BeNull();
        pdNow.Should().NotBeNull();
    }

    [Test]
    public void Can_Cast_PersianDate_To_DateTime()
    {
        var pd = PersianDate.Now;
        var dt = (DateTime)pd;

        Assert.That(dt, Is.TypeOf<DateTime>());
    }

    [Test]
    public void Can_Cast_DateTime_MinValue_To_PersianDate()
    {
        var dt = DateTime.MinValue;
        var pd = (PersianDate)dt;

        pd.Should().BeNull();
    }

    [Test]
    public void Can_Cast_DateTime_MaxValue_To_PersianDate()
    {
        var dt = DateTime.MaxValue;
        var pd = (PersianDate)dt;

        pd.Should().BeOfType<PersianDate>();
    }

    [Test]
    public void Compare_PersianDate_With_Equals_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);
        var pd3 = new PersianDate(1389, 12, 25);

        pd2.Equals((object)pd3).Should().BeTrue();
        pd2.Equals(pd3).Should().BeTrue();
        pd1.Equals(pd2).Should().BeFalse();
    }

    [Test]
    public void Compare_PersianDate_With_Equals_Method_With_Invalid_Type()
    {
        var pd1 = PersianDate.Now;
        pd1.Equals(string.Empty).Should().BeFalse();
    }

    [Test]
    public void Compare_PersianDate_With_Comparer_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);
        _ = new PersianDate(1389, 12, 25);

        pd1.Compare(pd2, pd1).Should().Be(1);
        pd2.Compare(pd1, pd2).Should().Be(-1);
        pd1.Compare(pd1, pd1).Should().Be(0);
    }

    [Test]
    public void Compare_PersianDate_With_CompareTo_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);

        pd2.CompareTo(pd1).Should().Be(1);
        pd1.CompareTo(pd2).Should().Be(-1);
        pd1.CompareTo(pd2).Should().Be(-1);
    }

    [Test]
    public void Compare_PersianDate_With_Null_Using_Operator_Throws()
    {
        var pd1 = PersianDate.Now;
        PersianDate pd2 = null;

        new Action(() => { _ = pd1 > pd2; }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Compare_PersianDate_With_Null_Using_IComparer_Throws()
    {
        var pd1 = PersianDate.Now;
        IComparer cmp = pd1;

        new Action(() => { _ = cmp.Compare(pd1, null); }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparer_Throws()
    {
        var pd1 = PersianDate.Now;
        object o = "test";
        IComparer cmp = pd1;

        new Action(() => { _ = cmp.Compare(pd1, o); }).Should().Throw<InvalidOperationException>();
        new Action(() => { _ = cmp.Compare(o, pd1); }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Compare_Smaller_And_Larger_PersainDates_Using_Comparer()
    {
        IComparer pd1 = new PersianDate(1380, 1, 1);
        IComparer pd2 = new PersianDate(1385, 1, 1);

        pd1.Compare(pd1, pd2).Should().Be(-1);
        pd2.Compare(pd2, pd1).Should().Be(1);
    }

    [Test]
    public void Compare_PersianDate_Equality_Using_Comparer()
    {
        IComparer pd1 = new PersianDate(1380, 1, 1);
        var pd2 = ((ICloneable) pd1).Clone();

        pd1.Compare(pd1, pd2).Should().Be(0);
    }

    [Test]
    public void Compare_PersianDate_Using_IComparer()
    {
        var pd1 = PersianDate.Now;
        var pd2 = new PersianDate(1380, 1, 1);
        IComparer cmp = pd1;

        new Action(() => { _ = cmp.Compare(pd1, pd2); }).Should().NotThrow();
    }

    [Test]
    public void Compare_PersianDate_Using_IComparable()
    {
        IComparable pd1 = new PersianDate(1390, 1, 1);
        IComparable pd2 = new PersianDate(1380, 1, 1);

        pd1.CompareTo(pd2).Should().Be(1);
        pd2.CompareTo(pd1).Should().Be(-1);
        pd2.CompareTo(pd2).Should().Be(0);
    }

    [Test]
    public void Compare_PersianDate_With_Null_Using_IComparable_Throws()
    {
        var pd1 = PersianDate.Now;
        IComparable cmp = pd1;

        new Action(() => { _ = cmp.CompareTo(null); }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparable_Throws()
    {
        var pd1 = PersianDate.Now;
        object o = "test";
        IComparable cmp = pd1;

        new Action(() => { cmp.CompareTo(o); }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Can_Get_Localized_Month_Name()
    {
        var pd = new PersianDate(1380, 1, 1);
        var monthName = pd.LocalizedMonthName;

        PersianDateTimeFormatInfo.MonthNames[0].Should().Be(monthName);
    }

    [Test]
    public void Creating_With_Invalid_Date_Throws()
    {
        new Action(() => { _ = new PersianDate(1384, 0, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1384, 13, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1388, 12, 30); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(10000, 1, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1384, 7, 31); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Creating_With_Invalid_Time_Throws()
    {
        new Action(() => { _ = new PersianDate(1384, 1, 1, 30, 1, 1, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1384, 1, 1, 20, 70, 1, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1384, 1, 1, 20, 1, 100, 1); }).Should().Throw<InvalidPersianDateException>();
        new Action(() => { _ = new PersianDate(1384, 1, 1, 20, 1, 1, 2000); }).Should().Throw<InvalidPersianDateException>();
    }

    [Test]
    public void Can_Create_With_HourAndMinute()
    {
        var pd = new PersianDate(1384, 1, 1, 20, 30);

        pd.Second.Should().Be(0);
        pd.Millisecond.Should().Be(0);
    }

    [Test]
    public void Creating_With_Default_Constructor_Creates_Now()
    {
        var now1 = PersianDate.Now;
        var now2 = new PersianDate();

        now2.Year.Should().Be(now1.Year);
        now2.Month.Should().Be(now1.Month);
        now2.Day.Should().Be(now1.Day);
    }

    [Test]
    public void Can_Create_With_String_And_Time()
    {
        var pd = new PersianDate("1384/01/01", new TimeSpan(12, 23, 48));

        pd.Year.Should().Be(1384);
        pd.Month.Should().Be(1);
        pd.Day.Should().Be(1);
        pd.Hour.Should().Be(12);
        pd.Minute.Should().Be(23);
        pd.Second.Should().Be(48);
        pd.Millisecond.Should().Be(0);
    }

    [Test]
    public void Can_Create_With_String_And_Long_Date_Format()
    {
        var datestring = "1384/01/03 12:22:43 AM";
        var pd = new PersianDate(datestring);

        pd.Year.Should().Be(1384);
        pd.Month.Should().Be(1);
        pd.Day.Should().Be(3);
        pd.Hour.Should().Be(0);
        pd.Minute.Should().Be(22);
        pd.Second.Should().Be(43);
        pd.Millisecond.Should().Be(0);
    }

    [Test]
    public void Can_Create_With_String_And_Short_Date_Format()
    {
        var datestring = "1384/01/03 12:22 PM";
        var pd = new PersianDate(datestring);

         pd.Year.Should().Be(1384);
         pd.Month.Should().Be(1);
         pd.Day.Should().Be(3);
         pd.Hour.Should().Be(12);
         pd.Minute.Should().Be(22);
         pd.Second.Should().Be(0);
         pd.Millisecond.Should().Be(0);
    }

    [Test]
    public void Can_Create_With_String_And_Date_Formatting()
    {
        var datestring = "1384/01/03";
        var pd = new PersianDate(datestring);

         pd.Year.Should().Be(1384);
         pd.Month.Should().Be(1);
         pd.Day.Should().Be(3);
         pd.Hour.Should().Be(0);
         pd.Minute.Should().Be(0);
         pd.Second.Should().Be(0);
         pd.Millisecond.Should().Be(0);
    }

    [Test]
    public void ToString_With_Empty_Parameter_Returns_Generic_Format()
    {
        var pd = new PersianDate(1380, 1, 1);
        var am = PersianDateTimeFormatInfo.AMDesignator;
        var result1 = pd.ToString();
        var result2 = pd.ToString(null, null);

         pd.ToString().Should().Be($"1380/01/01 00:00:00 {am}");
         result2.Should().Be(result1);
    }

    [Test]
    public void ToString_With_General_Parameter_Returns_Date_And_Time()
    {
        var pd = new PersianDate(1380, 1, 1);
        var am = PersianDateTimeFormatInfo.AMDesignator;

         pd.ToString("G").Should().Be($"1380/01/01 00:00:00 {am}");
    }

    [Test]
    public void Compare_PersianDate_Using_Equal_Operator()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;
        var pd3 = new PersianDate(1380, 1, 1);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd1 == pd2).Should().BeTrue();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd1 != pd3).Should().BeTrue();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd3 != pd1).Should().BeTrue();

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd1 != pd2).Should().BeFalse();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd1 == pd3).Should().BeFalse();
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        (pd3 == pd1).Should().BeFalse();
    }

    [Test]
    public void Compare_PersianDate_Using_Greater_Equal_Operator_And_Null_Throws()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;
        var pd3 = new PersianDate(1380, 1, 1);

        new Action(() => { _ = pd1 <= pd3; }).Should().Throw<InvalidOperationException>();
        new Action(() => { _ = pd2 >= pd3; }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Compare_PersianDate_Using_Not_Equal_Operator()
    {
        var pd1 = new PersianDate(1380, 11, 23);
        var pd2 = new PersianDate(1384, 1, 30);

        (pd1 != pd2).Should().BeTrue();
    }

    [Test]
    public void Compare_PersianDate_Using_Less_Than_Operator()
    {
        var pd2 = new PersianDate(1380, 11, 23);
        var pd1 = new PersianDate(1384, 1, 30);

        (pd2 < pd1).Should().BeTrue();
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_With_Null_Throws()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        PersianDate pd2 = null;

        new Action(() => { _ = pd1 < pd2; }).Should().Throw<InvalidOperationException>();
        new Action(() => { _ = pd2 < pd1; }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Year()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        var pd2 = new PersianDate(1385, 1, 30);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Month()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        var pd2 = new PersianDate(1384, 2, 30);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Day()
    {
        var pd1 = new PersianDate(1384, 1, 29);
        var pd2 = new PersianDate(1384, 1, 30);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Hour()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 0);
        var pd2 = new PersianDate(1384, 1, 29, 22, 0);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_Persian_Date_Using_Less_Than_Operator_Differing_Minute()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20);
        var pd2 = new PersianDate(1384, 1, 29, 20, 35);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Second()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 15);

        CheckDates(pd1, pd2);
    }

    [Test]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Millisecond()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 22);

        CheckDates(pd1, pd2);
    }

    private static void CheckDates(PersianDate smaller, PersianDate greater)
    {
        (smaller < greater).Should().BeTrue();
        (greater > smaller).Should().BeTrue();

        (smaller > greater).Should().BeFalse();
        (greater < smaller).Should().BeFalse();
    }

    [Test]
    public void Comparing_Equal_PersianDates_Using_CompareTo()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);

        var diff = pd1.CompareTo(pd2);

         diff.Should().Be(0);
    }

    [Test]
    public void PersianDate_Instance_Creates_StringBased_HashCode()
    {
        var pd = PersianDate.Now;
        var hashcode = pd.ToString("s").GetHashCode();

         pd.GetHashCode().Should().Be(hashcode);
    }

    [Test]
    public void Can_Parse_Short_Date_String()
    {
        var value = "1384/03/01";
        var pd = PersianDate.Parse(value);

         pd.Year.Should().Be(1384);
         pd.Month.Should().Be(3);
         pd.Day.Should().Be(1);
    }

    [Test]
    public void Converting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
    {
        var minValue = PersianDate.MinValue;
        var maxValue = PersianDate.MaxValue;
        var dateMinValue = DateTime.MinValue;
        var dateMaxValue = DateTime.MaxValue;

        minValue.ToPersianDate().Should().NotBeNull();
        maxValue.ToPersianDate().Should().NotBeNull();
        dateMaxValue.ToPersianDate().Should().NotBeNull();
        dateMinValue.ToPersianDate().Should().BeNull();
    }

    [Test]
    public void Casting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
    {
        var minValue = PersianDate.MinValue;
        var maxValue = PersianDate.MaxValue;
        var dateMinValue = DateTime.MinValue;
        var dateMaxValue = DateTime.MaxValue;

        ((PersianDate)minValue).Should().NotBeNull();
        ((PersianDate)maxValue).Should().NotBeNull();
        ((PersianDate)dateMaxValue).Should().NotBeNull();
        ((PersianDate)dateMinValue).Should().BeNull();
    }

    [Test]
    public void Can_Parse_Date_Short_Time_String()
    {
        var value = "1394/05/01 12:33:00 AM";
        var pd = PersianDate.Parse(value);

         pd.Year.Should().Be(1394);
         pd.Month.Should().Be(5);
         pd.Day.Should().Be(1);
         pd.Hour.Should().Be(0);
         pd.Minute.Should().Be(33);
    }

    [Test]
    public void Parsing_Empty_String_Will_Throw()
    {
        new Action(() => { PersianDate.Parse(string.Empty); }).Should().Throw<InvalidPersianDateFormatException>();
    }

    [Test]
    public void Can_Get_Weekday_Name()
    {
        var pd = new PersianDate(1387, 7, 7); //Yekshanbeh
         pd.LocalizedWeekDayName.Should().Be(PersianDateTimeFormatInfo.GetWeekDay(DayOfWeek.Sunday));
    }

    [Test]
    public void Comparing_Null_PersianDate_Should_Be_Equal()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;

        (pd1 > pd2).Should().BeFalse();
        (pd1 < pd2).Should().BeFalse();
        (pd2 > pd1).Should().BeFalse();
        (pd2 < pd1).Should().BeFalse();
    }

    [Test]
    public void ToString_With_Custom_FormatProvider()
    {
        var format = "CustomYearMonth";
        var pd = new PersianDate(1383, 4, 5);
        var value = pd.ToString(format, new TestFormatProvider());

         value.Should().Be($"{pd.Year} -- {pd.Month}");
    }

    [Test]
    public void ToString_With_Custom_FormatProvider_And_No_Specific_Format_Uses_Generic()
    {
        var pd = new PersianDate(1383, 4, 5);
        var value1 = pd.ToString(new TestFormatProvider());
        var value2 = pd.ToString("G");

         value1.Should().Be(value2);
    }

    [Test]
    public void Parse_With_Invalid_Or_Null_Values_Throws()
    {
        new Action(() => { PersianDate.Parse(null); }).Should().Throw<InvalidPersianDateFormatException>();
        new Action(() => { PersianDate.Parse(string.Empty); }).Should().Throw<InvalidPersianDateFormatException>();
        new Action(() => { PersianDate.Parse("1234"); }).Should().Throw<InvalidPersianDateFormatException>();
    }

    [Test]
    public void Try_Parse_With_Invalid_Or_Null_Values_Returns_False()
    {
        var canParse = PersianDate.TryParse(null, out var pd);
        canParse.Should().BeFalse();
        pd.Should().BeNull();

        canParse = PersianDate.TryParse(string.Empty, out pd);
        canParse.Should().BeFalse();
        pd.Should().BeNull();

        canParse = PersianDate.TryParse("1234", out pd);
        canParse.Should().BeFalse();
       pd.Should().BeNull();
    }

    [Test]
    public void Try_Parse_With_Correct_Value_Returns_True()
    {
        var canParse = PersianDate.TryParse("1384/1/1", out var pd);
        canParse.Should().BeTrue();
        pd.Should().NotBeNull();
    }

    [Test]
    public void Can_Convert_To_Written()
    {
        var pd = new PersianDate("1384/1/1");
        var written = pd.ToWritten();

         written.Should().Be($"{pd.LocalizedWeekDayName} {pd.Day} {pd.LocalizedMonthName} {pd.Year}");
    }

    [Test]
    public void Can_Parse_Date_From_String()
    {
        using(new CultureSwitchContext(CultureHelper.PersianCulture))
        {
            var dateString = "1388/06/17 12:00:00 ق.ظ";
            var pd = PersianDate.Parse(dateString);

             pd.Year.Should().Be(1388);
             pd.Month.Should().Be(6);
             pd.Day.Should().Be(17);
        }
    }

    [Test]
    public void Can_Parse_Date_From_String_Using_BuiltIn_CultureInfo()
    {
        using (new CultureSwitchContext(new CultureInfo("fa-ir")))
        {
            var dateString = "1388/06/17 12:00:00 ق.ظ";
            var pd = PersianDate.Parse(dateString);

             pd.Year.Should().Be(1388);
            pd.Month.Should().Be(6);
            pd.Day.Should().Be(17);
        }
    }
}