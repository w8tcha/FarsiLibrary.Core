using PersianCalendar=FarsiLibrary.Core.Utils.PersianCalendar;

namespace FarsiLibrary.Tests;

using System;
using System.Linq;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Tests.Helpers;

public class PersianCultureInfoTests
{
    [Fact]
    public void Can_Create_CultureInfo()
    {
        var ci = new PersianCultureInfo();
        Assert.NotNull(ci);
    }

    [Fact]
    public void Can_Set_Thread_Culture()
    {
        PersianCultureInfo ci;

        using (new CultureSwitchContext(ci = new PersianCultureInfo()))
        {
            Assert.Equal(ci, System.Threading.Thread.CurrentThread.CurrentUICulture);
            Assert.Equal(ci, System.Threading.Thread.CurrentThread.CurrentCulture);
        }
    }

    [Fact]
    public void Creating_PersianCultureInfo_Will_Set_Correct_Calendar()
    {
        var ci = new PersianCultureInfo();

        Assert.IsNotType<PersianCalendar>(ci.Calendar);
        Assert.True(PersianCultureInfo.IsReadOnly);
    }

    [Fact]
    public void Setting_ThreadCulture_To_PersianCulture_Will_Set_Correct_Calendar()
    {
        using (new CultureSwitchContext(new PersianCultureInfo()))
        {
            Assert.IsNotType<PersianCalendar>(System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar);
            Assert.IsType<System.Globalization.PersianCalendar>(System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar);
        }
    }

    [Fact]
    public void Can_Clone_CultureInfo()
    {
        var original = new PersianCultureInfo();
        var clone = original.Clone();

        Assert.NotSame(original, clone);
        Assert.Equal(original, clone);
    }

    [Fact]
    public void Optional_Calendar_Contains_CorrectCalendars()
    {
        var ci = new PersianCultureInfo();
        var localCalendar = ci.OptionalCalendars.OfType<PersianCalendar>().FirstOrDefault();
        var frameworkCalendar = ci.OptionalCalendars.OfType<System.Globalization.PersianCalendar>().FirstOrDefault();

        Assert.NotNull(frameworkCalendar);
        Assert.NotNull(localCalendar);
    }

    [Fact]
    public void Setting_DateFormat_To_Null_Throws()
    {
        var ci = new PersianCultureInfo();

        Assert.Throws<ArgumentNullException>(() => ci.DateTimeFormat = null!);
    }

    [Fact]
    public void Can_Set_DateTimeFormat_To_Other_Instances()
    {
        var ci = new PersianCultureInfo();

        var exception = Record.Exception(() => ci.DateTimeFormat = DateTimeFormatWrapper.GetFormatInfo());
        Assert.Null(exception);
    }

    [Fact]
    public void Can_Convert_To_String_With_InvariantCulture()
    {
        using (new CultureSwitchContext(new PersianCultureInfo()))
        {
            var dt = DateTime.MinValue;

            var exception = Record.Exception(() => dt.ToString(CultureInfo.InvariantCulture));
            Assert.Null(exception);
        }
    }

    [Fact]
    public void Converts_To_Correct_DayOfWeek()
    {
        PersianDate pd = new DateTime(2008, 10, 17);
        var dt = new DateTime(2008, 10, 18, 0, 0, 0, new System.Globalization.PersianCalendar());

        Assert.Equal(DayOfWeek.Friday, pd.DayOfWeek);
        Assert.Equal(DayOfWeek.Friday, dt.DayOfWeek);
    }

    [Fact]
    public void Converts_To_Correct_DayOfWeek_String()
    {
        var ci = new CultureInfo("fa-ir");
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var dt1 = new DateTime(2008, 10, 17);
            var dt2 = new DateTime(1387, 7, 26, 0, 0, 0, new System.Globalization.PersianCalendar());

            Assert.Equal(dt1.ToString("dddd"), dt1.ToString("dddd"));
            Assert.Equal(dt1.ToString("dddd", ci), dt2.ToString("dddd", ci));
            Assert.Equal(dt1.ToString("dddd", cip), dt2.ToString("dddd", cip));
        }
    }

    [Fact]
    public void Should_Return_Right_DayOfWeek_Translation()
    {
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var friday = cip.DateTimeFormat.GetDayName(DayOfWeek.Friday);
            Assert.Equal("جمعه", friday);
        }
    }

    [Fact]
    public void Seven_DaysOfWeek_Index_Value_Validity_In_PersianCalendar()
    {
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var pd1 = new DateTime(1387, 8, 2, cip.Calendar);
            Assert.Equal(DayOfWeek.Thursday, pd1.DayOfWeek);

            var pd2 = new DateTime(1387, 8, 3, cip.Calendar);
            Assert.Equal(DayOfWeek.Friday, pd2.DayOfWeek);

            var pd3 = new DateTime(1387, 8, 4, cip.Calendar);
            Assert.Equal(DayOfWeek.Saturday, pd3.DayOfWeek);

            var pd4 = new DateTime(1387, 8, 5, cip.Calendar);
            Assert.Equal(DayOfWeek.Sunday, pd4.DayOfWeek);

            var pd5 = new DateTime(1387, 8, 6, cip.Calendar);
            Assert.Equal(DayOfWeek.Monday, pd5.DayOfWeek);

            var pd6 = new DateTime(1387, 8, 7, cip.Calendar);
            Assert.Equal(DayOfWeek.Tuesday, pd6.DayOfWeek);

            var pd7 = new DateTime(1387, 8, 8, cip.Calendar);
            Assert.Equal(DayOfWeek.Wednesday, pd7.DayOfWeek);
        }
    }

    [Fact]
    public void Can_Create_Readonly_Copy()
    {
        var cip = new PersianCultureInfo();

        var exception = Record.Exception(() => CultureInfo.ReadOnly(cip));
        Assert.Null(exception);
    }

    [Fact]
    public void Setting_Culture_To_PersianCultureInfo_Will_Set_DateTimeFormat()
    {
        var cip = new PersianCultureInfo();
        var format = cip.CreateDateTimeFormatInfo();

        using(new CultureSwitchContext(cip))
        {
            Assert.NotNull(cip.DateTimeFormat);
            Assert.Equal(format.AbbreviatedDayNames, cip.DateTimeFormat.AbbreviatedDayNames);
            Assert.Equal(format.AbbreviatedMonthGenitiveNames, cip.DateTimeFormat.AbbreviatedMonthGenitiveNames);
            Assert.Equal(format.AbbreviatedMonthNames, cip.DateTimeFormat.AbbreviatedMonthNames);
            Assert.Equal(format.AMDesignator, cip.DateTimeFormat.AMDesignator);
            Assert.Equal(format.PMDesignator, cip.DateTimeFormat.PMDesignator);
            Assert.Equal(format.Calendar, cip.DateTimeFormat.Calendar);
            Assert.Equal(format.DayNames, cip.DateTimeFormat.DayNames);
            Assert.Equal(format.DateSeparator, cip.DateTimeFormat.DateSeparator);
            Assert.Equal(format.ShortDatePattern, cip.DateTimeFormat.ShortDatePattern);
            Assert.Equal(format.ShortestDayNames, cip.DateTimeFormat.ShortestDayNames);
            Assert.Equal(format.ShortTimePattern, cip.DateTimeFormat.ShortTimePattern);
            Assert.Equal(format.YearMonthPattern, cip.DateTimeFormat.YearMonthPattern);
            Assert.Equal(format.TimeSeparator, cip.DateTimeFormat.TimeSeparator);
        }
    }
}