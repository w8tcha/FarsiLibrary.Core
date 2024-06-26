namespace FarsiLibrary.Tests;

using System;
using System.Collections;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Exceptions;
using FarsiLibrary.Core.Utils.Internals;
using FarsiLibrary.Tests.Helpers;

public class PersianDateTest
{
    [Fact]
    public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_Invariant_Thread_Culture()
    {
        var exception = Record.Exception(() => PersianDate.Parse("1388/02/30"));
        Assert.Null(exception);
    }

    [Fact]
    public void Parsing_30th_Of_Ordibehesht_Should_Not_Fail_When_Using_System_Persian_Culture()
    {
        using(new CultureSwitchContext(CultureHelper.FarsiCulture))
        {
            var exception = Record.Exception(() => PersianDate.Parse("1388/02/30"));
            Assert.Null(exception);
            
            exception = Record.Exception(() => PersianDate.Parse("1388/02/31"));
            Assert.Null(exception);
        }
    }

    [Fact]
    public void Parsing_With_Years_Less_Than_Two_Digits_Will_Use_Four_Year_Digits()
    {
        using(new CultureSwitchContext(CultureHelper.FarsiCulture))
        {
            var pd = PersianDate.Parse("88/2/3");

            Assert.Equal(1388,pd.Year);
            Assert.Equal(2,pd.Month);
            Assert.Equal(3,pd.Day);
        }
    }

    [Fact]
    public void Seven_Days_Of_Week_Validity()
    {
        var pd1 = new PersianDate(1386, 6, 1);
        Assert.Equal(DayOfWeek.Thursday, pd1.DayOfWeek);

        var pd2 = new PersianDate(1386, 6, 2);
        Assert.Equal(DayOfWeek.Friday, pd2.DayOfWeek);

        var pd3 = new PersianDate(1386, 6, 3);
        Assert.Equal(DayOfWeek.Saturday, pd3.DayOfWeek);

        var pd4 = new PersianDate(1386, 6, 4);
        Assert.Equal(DayOfWeek.Sunday, pd4.DayOfWeek);

        var pd5 = new PersianDate(1386, 6, 5);
        Assert.Equal(DayOfWeek.Monday, pd5.DayOfWeek);

        var pd6 = new PersianDate(1386, 6, 6);
        Assert.Equal(DayOfWeek.Tuesday, pd6.DayOfWeek);

        var pd7 = new PersianDate(1386, 6, 7);
        Assert.Equal(DayOfWeek.Wednesday, pd7.DayOfWeek);
    }

    [Fact]
    public void Seven_DaysOfWeek_Index_Value_Validity()
    {
        var pd1 = new PersianDate(1387, 8, 2);
        Assert.Equal(DayOfWeek.Thursday, pd1.DayOfWeek);

        var pd2 = new PersianDate(1387, 8, 3);
        Assert.Equal(DayOfWeek.Friday, pd2.DayOfWeek);

        var pd3 = new PersianDate(1387, 8, 4);
        Assert.Equal(DayOfWeek.Saturday, pd3.DayOfWeek);

        var pd4 = new PersianDate(1387, 8, 5);
        Assert.Equal(DayOfWeek.Sunday, pd4.DayOfWeek);

        var pd5 = new PersianDate(1387, 8, 6);
        Assert.Equal(DayOfWeek.Monday, pd5.DayOfWeek);

        var pd6 = new PersianDate(1387, 8, 7);
        Assert.Equal(DayOfWeek.Tuesday, pd6.DayOfWeek);

        var pd7 = new PersianDate(1387, 8, 8);
        Assert.Equal(DayOfWeek.Wednesday, pd7.DayOfWeek);
    }

    [Fact]
    public void Invalid_Day_In_PersianDate_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1387, 1, 32));
    }

    [Fact]
    public void Invalid_Year_In_PersianDate_Throws()
    {
        _ = new PersianDate(DateTime.MinValue.Year, 1, 1);
    }

    [Fact]
    public void Can_Create_Minimum_PersianDate_Year_Value()
    {
        _ = new PersianDate(PersianDate.MinValue.Year, 1, 1);
    }

    [Fact]
    public void Can_Create_Maximum_PersianDate_Year_Value()
    {
        _ = new PersianDate(PersianDate.MaxValue.Year, 1, 1);
    }

    [Fact]
    public void Can_Cast_DateTime_To_PersianDate()
    {
        var dt = DateTime.Now;
        var pd = (PersianDate)dt;

        Assert.NotNull(pd);
    }

    [Fact]
    public void Can_Cast_Nullable_DateTime_To_PersianDate()
    {
        DateTime? dtNow = DateTime.Now;

        PersianDate pdNull = (DateTime?)null;
        PersianDate pdNow = dtNow;

        Assert.Null(pdNull);
        Assert.NotNull(pdNow);
    }

    [Fact]
    public void Can_Cast_PersianDate_To_DateTime()
    {
        var pd = PersianDate.Now;
        var dt = (DateTime)pd;

        Assert.IsType<DateTime>(dt);
    }

    [Fact]
    public void Can_Cast_DateTime_MinValue_To_PersianDate()
    {
        var dt = DateTime.MinValue;
        var pd = (PersianDate)dt;

        Assert.Null(pd);
    }

    [Fact]
    public void Can_Cast_DateTime_MaxValue_To_PersianDate()
    {
        var dt = DateTime.MaxValue;
        var pd = (PersianDate)dt;

        Assert.IsType<PersianDate>(pd);
    }

    [Fact]
    public void Compare_PersianDate_With_Equals_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);
        var pd3 = new PersianDate(1389, 12, 25);

        Assert.True(pd2.Equals((object)pd3));
        Assert.True(pd2.Equals(pd3));
        Assert.False(pd1.Equals(pd2));
    }

    [Fact]
    public void Compare_PersianDate_With_Equals_Method_With_Invalid_Type()
    {
        var pd1 = PersianDate.Now;
        Assert.False(pd1.Equals(string.Empty));
    }

    [Fact]
    public void Compare_PersianDate_With_Comparer_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);
        _ = new PersianDate(1389, 12, 25);

        Assert.Equal(1, pd1.Compare(pd2, pd1));
        Assert.Equal(-1, pd2.Compare(pd1, pd2));
        Assert.Equal(0, pd1.Compare(pd1, pd1));
    }

    [Fact]
    public void Compare_PersianDate_With_CompareTo_Method()
    {
        var pd1 = new PersianDate(1384, 10, 1);
        var pd2 = new PersianDate(1389, 12, 25);

        Assert.Equal(1, pd2.CompareTo(pd1));
        Assert.Equal(-1, pd1.CompareTo(pd2));
        Assert.Equal(-1, pd1.CompareTo(pd2));
    }

    [Fact]
    public void Compare_PersianDate_With_Null_Using_Operator_Throws()
    {
        var pd1 = PersianDate.Now;
        PersianDate pd2 = null;

        Assert.Throws<InvalidOperationException>(() => { _ = pd1 > pd2; });
    }

    [Fact]
    public void Compare_PersianDate_With_Null_Using_IComparer_Throws()
    {
        var pd1 = PersianDate.Now;
        IComparer cmp = pd1;

        Assert.Throws<InvalidOperationException>(() => cmp.Compare(pd1, null));
    }

    [Fact]
    public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparer_Throws()
    {
        var pd1 = PersianDate.Now;
        object o = "test";
        IComparer cmp = pd1;

        Assert.Throws<InvalidOperationException>(() => cmp.Compare(pd1, o));
        Assert.Throws<InvalidOperationException>(() => cmp.Compare(o, pd1));
    }

    [Fact]
    public void Compare_Smaller_And_Larger_PersainDates_Using_Comparer()
    {
        var pd1 = (IComparer)new PersianDate(1380, 1, 1);
        var pd2 = (IComparer)new PersianDate(1385, 1, 1);

        Assert.Equal(-1, pd1.Compare(pd1, pd2));
        Assert.Equal(1, pd2.Compare(pd2, pd1));
    }

    [Fact]
    public void Compare_PersianDate_Equality_Using_Comparer()
    {
        var pd1 = (IComparer)new PersianDate(1380, 1, 1);
        var pd2 = ((ICloneable) pd1).Clone();

        Assert.Equal(0, pd1.Compare(pd1, pd2));
    }

    [Fact]
    public void Compare_PersianDate_Using_IComparer()
    {
        var pd1 = PersianDate.Now;
        var pd2 = new PersianDate(1380, 1, 1);
        IComparer cmp = pd1;

        _ = cmp.Compare(pd1, pd2);
    }

    [Fact]
    public void Compare_PersianDate_Using_IComparable()
    {
        var pd1 = (IComparable)new PersianDate(1390, 1, 1);
        var pd2 = (IComparable)new PersianDate(1380, 1, 1);

        Assert.Equal(1, pd1.CompareTo(pd2));
        Assert.Equal(-1, pd2.CompareTo(pd1));
        Assert.Equal(0, pd2.CompareTo(pd2));
    }

    [Fact]
    public void Compare_PersianDate_With_Null_Using_IComparable_Throws()
    {
        var pd1 = PersianDate.Now;
        IComparable cmp = pd1;

        Assert.Throws<InvalidOperationException>(() => cmp.CompareTo(null));
    }

    [Fact]
    public void Compare_PersianDate_With_Invalid_Object_Type_Using_IComparable_Throws()
    {
        var pd1 = PersianDate.Now;
        object o = "test";
        IComparable cmp = pd1;

        Assert.Throws<InvalidOperationException>(() => cmp.CompareTo(o));
    }

    [Fact]
    public void Can_Get_Localized_Month_Name()
    {
        var pd = new PersianDate(1380, 1, 1);
        var monthName = pd.LocalizedMonthName;

        Assert.Equal(monthName, PersianDateTimeFormatInfo.MonthNames[0]);
    }

    [Fact]
    public void Creating_With_Invalid_Date_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 0, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 13, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1388, 12, 30));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(10000, 1, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 7, 31));
    }

    [Fact]
    public void Creating_With_Invalid_Time_Throws()
    {
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 30, 1, 1, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 70, 1, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 1, 100, 1));
        Assert.Throws<InvalidPersianDateException>(() => new PersianDate(1384, 1, 1, 20, 1, 1, 2000));
    }

    [Fact]
    public void Can_Create_With_HourAndMinute()
    {
        var pd = new PersianDate(1384, 1, 1, 20, 30);

        Assert.Equal(0, pd.Second);
        Assert.Equal(0, pd.Millisecond);
    }

    [Fact]
    public void Creating_With_Default_Constructor_Creates_Now()
    {
        var now1 = PersianDate.Now;
        var now2 = new PersianDate();

        Assert.Equal(now1.Year, now2.Year);
        Assert.Equal(now1.Month, now2.Month);
        Assert.Equal(now1.Day, now2.Day);
    }

    [Fact]
    public void Can_Create_With_String_And_Time()
    {
        var pd = new PersianDate("1384/01/01", new TimeSpan(12, 23, 48));

        Assert.Equal(1384, pd.Year);
        Assert.Equal(1, pd.Month);
        Assert.Equal(1, pd.Day);
        Assert.Equal(12, pd.Hour);
        Assert.Equal(23, pd.Minute);
        Assert.Equal(48, pd.Second);
        Assert.Equal(0, pd.Millisecond);
    }

    [Fact]
    public void Can_Create_With_String_And_Long_Date_Format()
    {
        var datestring = "1384/01/03 12:22:43 AM";
        var pd = new PersianDate(datestring);

        Assert.Equal(1384, pd.Year);
        Assert.Equal(1, pd.Month);
        Assert.Equal(3, pd.Day);
        Assert.Equal(0, pd.Hour);
        Assert.Equal(22, pd.Minute);
        Assert.Equal(43, pd.Second);
        Assert.Equal(0, pd.Millisecond);
    }

    [Fact]
    public void Can_Create_With_String_And_Short_Date_Format()
    {
        var datestring = "1384/01/03 12:22 PM";
        var pd = new PersianDate(datestring);

        Assert.Equal(1384, pd.Year);
        Assert.Equal(1, pd.Month);
        Assert.Equal(3, pd.Day);
        Assert.Equal(12, pd.Hour);
        Assert.Equal(22, pd.Minute);
        Assert.Equal(0, pd.Second);
        Assert.Equal(0, pd.Millisecond);
    }

    [Fact]
    public void Can_Create_With_String_And_Date_Formatting()
    {
        var datestring = "1384/01/03";
        var pd = new PersianDate(datestring);

        Assert.Equal(1384, pd.Year);
        Assert.Equal(1, pd.Month);
        Assert.Equal(3, pd.Day);
        Assert.Equal(0, pd.Hour);
        Assert.Equal(0, pd.Minute);
        Assert.Equal(0, pd.Second);
        Assert.Equal(0, pd.Millisecond);
    }

    [Fact]
    public void ToString_With_Empty_Parameter_Returns_Generic_Format()
    {
        var pd = new PersianDate(1380, 1, 1);
        var am = PersianDateTimeFormatInfo.AMDesignator;
        var result1 = pd.ToString();
        var result2 = pd.ToString(null, null);

        Assert.Equal("1380/01/01 00:00:00 " + am, pd.ToString());
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void ToString_With_General_Parameter_Returns_Date_And_Time()
    {
        var pd = new PersianDate(1380, 1, 1);
        var am = PersianDateTimeFormatInfo.AMDesignator;

        Assert.Equal("1380/01/01 00:00:00 " + am, pd.ToString("G"));
    }

    [Fact]
    public void Compare_PersianDate_Using_Equal_Operator()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;
        var pd3 = new PersianDate(1380, 1, 1);

        Assert.True(pd1 == pd2);
        Assert.True(pd1 != pd3);
        Assert.True(pd3 != pd1);

        Assert.False(pd1 != pd2);
        Assert.False(pd1 == pd3);
        Assert.False(pd3 == pd1);
    }

    [Fact]
    public void Compare_PersianDate_Using_Greater_Equal_Operator_And_Null_Throws()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;
        var pd3 = new PersianDate(1380, 1, 1);

        Assert.Throws<InvalidOperationException>(() => pd1 <= pd3);
        Assert.Throws<InvalidOperationException>(() => pd2 >= pd3);
    }

    [Fact]
    public void Compare_PersianDate_Using_Not_Equal_Operator()
    {
        var pd1 = new PersianDate(1380, 11, 23);
        var pd2 = new PersianDate(1384, 1, 30);

        Assert.True(pd1 != pd2);
    }

    [Fact]
    public void Compare_PersianDate_Using_Less_Than_Operator()
    {
        var pd2 = new PersianDate(1380, 11, 23);
        var pd1 = new PersianDate(1384, 1, 30);

        Assert.True(pd2 < pd1);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_With_Null_Throws()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        PersianDate pd2 = null;
        bool result;

        Assert.Throws<InvalidOperationException>(() => result = pd1 < pd2);
        Assert.Throws<InvalidOperationException>(() => result = pd2 < pd1);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Year()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        var pd2 = new PersianDate(1385, 1, 30);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Month()
    {
        var pd1 = new PersianDate(1384, 1, 30);
        var pd2 = new PersianDate(1384, 2, 30);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Day()
    {
        var pd1 = new PersianDate(1384, 1, 29);
        var pd2 = new PersianDate(1384, 1, 30);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Hour()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 0);
        var pd2 = new PersianDate(1384, 1, 29, 22, 0);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_Persian_Date_Using_Less_Than_Operator_Differing_Minute()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20);
        var pd2 = new PersianDate(1384, 1, 29, 20, 35);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Second()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 15);

        CheckDates(pd1, pd2);
    }

    [Fact]
    public void Comparing_PersianDate_Using_Less_Than_Operator_Differing_Millisecond()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 22);

        CheckDates(pd1, pd2);
    }

    private static void CheckDates(PersianDate smaller, PersianDate greater)
    {
        Assert.True(smaller < greater);
        Assert.True(greater > smaller);

        Assert.False(smaller > greater);
        Assert.False(greater < smaller);            
    }

    [Fact]
    public void Comparing_Equal_PersianDates_Using_CompareTo()
    {
        var pd1 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);
        var pd2 = new PersianDate(1384, 1, 29, 20, 20, 10, 21);

        var diff = pd1.CompareTo(pd2);

        Assert.Equal(0, diff);
    }

    [Fact]
    public void PersianDate_Instance_Creates_StringBased_HashCode()
    {
        var pd = PersianDate.Now;
        var hashcode = pd.ToString("s").GetHashCode();

        Assert.Equal(hashcode, pd.GetHashCode());
    }

    [Fact]
    public void Can_Parse_Short_Date_String()
    {
        var value = "1384/03/01";
        var pd = PersianDate.Parse(value);

        Assert.Equal(1384, pd.Year);
        Assert.Equal(3, pd.Month);
        Assert.Equal(1, pd.Day);
    }

    [Fact]
    public void Converting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
    {
        var minValue = PersianDate.MinValue;
        var maxValue = PersianDate.MaxValue;
        var dateMinValue = DateTime.MinValue;
        var dateMaxValue = DateTime.MaxValue;

        Assert.NotNull(minValue.ToPersianDate());
        Assert.NotNull(maxValue.ToPersianDate());
        Assert.NotNull(dateMaxValue.ToPersianDate());
        Assert.Null(dateMinValue.ToPersianDate());
    }

    [Fact]
    public void Casting_Out_Of_Range_DateTime_Instances_Will_Return_Null()
    {
        var minValue = PersianDate.MinValue;
        var maxValue = PersianDate.MaxValue;
        var dateMinValue = DateTime.MinValue;
        var dateMaxValue = DateTime.MaxValue;

        Assert.NotNull((PersianDate)minValue);
        Assert.NotNull((PersianDate)maxValue);
        Assert.NotNull((PersianDate)dateMaxValue);
        Assert.Null((PersianDate)dateMinValue);            
    }

    [Fact]
    public void Can_Parse_Date_Short_Time_String()
    {
        var value = "1394/05/01 12:33:00 AM";
        var pd = PersianDate.Parse(value);

        Assert.Equal(1394, pd.Year);
        Assert.Equal(5, pd.Month);
        Assert.Equal(1, pd.Day);
        Assert.Equal(0, pd.Hour);
        Assert.Equal(33, pd.Minute);
    }

    [Fact]
    public void Parsing_Empty_String_Will_Throw()
    {
        Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(string.Empty));
    }

    [Fact]
    public void Can_Get_Weekday_Name()
    {
        var pd = new PersianDate(1387, 7, 7); //Yekshanbeh
        Assert.Equal(PersianDateTimeFormatInfo.GetWeekDay(DayOfWeek.Sunday), pd.LocalizedWeekDayName);
    }

    [Fact]
    public void Comparing_Null_PersianDate_Should_Be_Equal()
    {
        PersianDate pd1 = null;
        PersianDate pd2 = null;

        Assert.False(pd1 > pd2);
        Assert.False(pd1 < pd2);
        Assert.False(pd2 > pd1);
        Assert.False(pd2 < pd1);
    }

    [Fact]
    public void ToString_With_Custom_FormatProvider()
    {
        var format = "CustomYearMonth";
        var pd = new PersianDate(1383, 4, 5);
        var value = pd.ToString(format, new TestFormatProvider());

        Assert.Equal(pd.Year + " -- " + pd.Month, value);
    }

    [Fact]
    public void ToString_With_Custom_FormatProvider_And_No_Specific_Format_Uses_Generic()
    {
        var pd = new PersianDate(1383, 4, 5);
        var value1 = pd.ToString(new TestFormatProvider());
        var value2 = pd.ToString("G");

        Assert.Equal(value2, value1);
    }

    [Fact]
    public void Parse_With_Invalid_Or_Null_Values_Throws()
    {
        Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(null));
        Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse(string.Empty));
        Assert.Throws<InvalidPersianDateFormatException>(() => PersianDate.Parse("1234"));
    }

    [Fact]
    public void Try_Parse_With_Invalid_Or_Null_Values_Returns_False()
    {
        var canParse = PersianDate.TryParse(null, out var pd);
        Assert.False(canParse);
        Assert.Null(pd);

        canParse = PersianDate.TryParse(string.Empty, out pd);
        Assert.False(canParse);
        Assert.Null(pd);

        canParse = PersianDate.TryParse("1234", out pd);
        Assert.False(canParse);
        Assert.Null(pd);
    }

    [Fact]
    public void Try_Parse_With_Correct_Value_Returns_True()
    {
        var canParse = PersianDate.TryParse("1384/1/1", out var pd);
        Assert.True(canParse);
        Assert.NotNull(pd);
    }

    [Fact]
    public void Can_Convert_To_Written()
    {
        var pd = new PersianDate("1384/1/1");
        var written = pd.ToWritten();

        Assert.Equal($"{pd.LocalizedWeekDayName} {pd.Day} {pd.LocalizedMonthName} {pd.Year}", written);
    }

    [Fact]
    public void Can_Parse_Date_From_String()
    {
        using(new CultureSwitchContext(CultureHelper.PersianCulture))
        {
            var dateString = "1388/06/17 12:00:00 ق.ظ";
            var pd = PersianDate.Parse(dateString);

            Assert.Equal(1388, pd.Year);
            Assert.Equal(6, pd.Month);
            Assert.Equal(17, pd.Day);
        }
    }

    [Fact]
    public void Can_Parse_Date_From_String_Using_BuiltIn_CultureInfo()
    {
        using (new CultureSwitchContext(new CultureInfo("fa-ir")))
        {
            var dateString = "1388/06/17 12:00:00 ق.ظ";
            var pd = PersianDate.Parse(dateString);

            Assert.Equal(1388, pd.Year);
            Assert.Equal(6,pd.Month);
            Assert.Equal(17,pd.Day);
        }
    }
}