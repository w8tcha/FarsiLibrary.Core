namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Internals;
using FarsiLibrary.Tests.Helpers;

using System;
using System.Linq;

public class PersianCultureInfoTests
{
    [Test]
    public void Can_Create_CultureInfo()
    {
        var ci = new PersianCultureInfo();
        ci.Should().NotBeNull();
    }

    [Test]
    public void Can_Set_Thread_Culture()
    {
        PersianCultureInfo ci;

        using (new CultureSwitchContext(ci = new PersianCultureInfo()))
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture.Should().Be(ci);
            System.Threading.Thread.CurrentThread.CurrentCulture.Should().Be(ci);
        }
    }

    [Test]
    public void Creating_PersianCultureInfo_Will_Set_Correct_Calendar()
    {
        var ci = new PersianCultureInfo();

        ci.Calendar.Should().NotBeOfType<PersianCalendar>();
        (PersianCultureInfo.IsReadOnly).Should().BeTrue();
    }

    [Test]
    public void Setting_ThreadCulture_To_PersianCulture_Will_Set_Correct_Calendar()
    {
        using (new CultureSwitchContext(new PersianCultureInfo()))
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.Should().NotBeOfType<PersianCalendar>();
            System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.Should().BeOfType<System.Globalization.PersianCalendar>();
        }
    }

    [Test]
    public void Can_Clone_CultureInfo()
    {
        var original = new PersianCultureInfo();
        var clone = original.Clone();

        clone.Should().NotBeSameAs(original);
        clone.Should().Be(original);
    }

    [Test]
    public void Optional_Calendar_Contains_CorrectCalendars()
    {
        var ci = new PersianCultureInfo();
        var localCalendar = ci.OptionalCalendars.OfType<PersianCalendar>().FirstOrDefault();
        var frameworkCalendar = ci.OptionalCalendars.OfType<System.Globalization.PersianCalendar>().FirstOrDefault();

        frameworkCalendar.Should().NotBeNull();
        localCalendar.Should().NotBeNull();
    }

    [Test]
    public void Setting_DateFormat_To_Null_Throws()
    {
        var ci = new PersianCultureInfo();

        new Action(() => { ci.DateTimeFormat = null!; }).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Can_Set_DateTimeFormat_To_Other_Instances()
    {
        var ci = new PersianCultureInfo();

        var exception = () => { ci.DateTimeFormat = DateTimeFormatWrapper.GetFormatInfo(); };
        exception.Should().NotThrow();
    }

    [Test]
    public void Can_Convert_To_String_With_InvariantCulture()
    {
        using (new CultureSwitchContext(new PersianCultureInfo()))
        {
            var dt = DateTime.MinValue;

            var exception = () => { dt.ToString(CultureInfo.InvariantCulture); };
            exception.Should().NotThrow();
        }
    }

    [Test]
    public void Converts_To_Correct_DayOfWeek()
    {
        PersianDate pd = new DateTime(2008, 10, 17);
        var dt = new DateTime(2008, 10, 18, 0, 0, 0, new System.Globalization.PersianCalendar());

        pd.DayOfWeek.Should().Be(DayOfWeek.Friday);
        dt.DayOfWeek.Should().Be(DayOfWeek.Friday);
    }

    [Test]
    public void Converts_To_Correct_DayOfWeek_String()
    {
        var ci = new CultureInfo("fa-ir");
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var dt1 = new DateTime(2008, 10, 17);
            var dt2 = new DateTime(1387, 7, 26, 0, 0, 0, new System.Globalization.PersianCalendar());

            dt1.ToString("dddd").Should().Be(dt1.ToString("dddd"));
            dt2.ToString("dddd", ci).Should().Be(dt1.ToString("dddd"));
            dt2.ToString("dddd", cip).Should().Be(dt1.ToString("dddd"));
        }
    }

    [Test]
    public void Should_Return_Right_DayOfWeek_Translation()
    {
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var friday = cip.DateTimeFormat.GetDayName(DayOfWeek.Friday);
            friday.Should().Be("جمعه");
        }
    }

    [Test]
    public void Seven_DaysOfWeek_Index_Value_Validity_In_PersianCalendar()
    {
        var cip = new PersianCultureInfo();

        using (new CultureSwitchContext(cip))
        {
            var pd1 = new DateTime(1387, 8, 2, cip.Calendar);
            pd1.DayOfWeek.Should().Be(DayOfWeek.Thursday);

            var pd2 = new DateTime(1387, 8, 3, cip.Calendar);
            pd2.DayOfWeek.Should().Be(DayOfWeek.Friday);

            var pd3 = new DateTime(1387, 8, 4, cip.Calendar);
            pd3.DayOfWeek.Should().Be(DayOfWeek.Saturday);

            var pd4 = new DateTime(1387, 8, 5, cip.Calendar);
            pd4.DayOfWeek.Should().Be(DayOfWeek.Sunday);

            var pd5 = new DateTime(1387, 8, 6, cip.Calendar);
            pd5.DayOfWeek.Should().Be(DayOfWeek.Monday);

            var pd6 = new DateTime(1387, 8, 7, cip.Calendar);
            pd6.DayOfWeek.Should().Be(DayOfWeek.Tuesday);

            var pd7 = new DateTime(1387, 8, 8, cip.Calendar);
            pd7.DayOfWeek.Should().Be(DayOfWeek.Wednesday);
        }
    }

    [Test]
    public void Can_Create_Readonly_Copy()
    {
        var cip = new PersianCultureInfo();

        var exception = () => { CultureInfo.ReadOnly(cip); };
        exception.Should().NotThrow();
    }

    [Test]
    public void Setting_Culture_To_PersianCultureInfo_Will_Set_DateTimeFormat()
    {
        var cip = new PersianCultureInfo();
        var format = cip.CreateDateTimeFormatInfo();

        using(new CultureSwitchContext(cip))
        {
            cip.DateTimeFormat.Should().NotBeNull();
            cip.DateTimeFormat.AbbreviatedDayNames.Should().BeEquivalentTo(format.AbbreviatedDayNames);
            cip.DateTimeFormat.AbbreviatedMonthGenitiveNames.Should().BeEquivalentTo(format.AbbreviatedMonthGenitiveNames);
            cip.DateTimeFormat.AbbreviatedMonthNames.Should().BeEquivalentTo(format.AbbreviatedMonthNames);
            cip.DateTimeFormat.AMDesignator.Should().Be(format.AMDesignator);
            cip.DateTimeFormat.PMDesignator.Should().Be(format.PMDesignator);
            cip.DateTimeFormat.Calendar.Should().Be(format.Calendar);
            cip.DateTimeFormat.DayNames.Should().BeEquivalentTo(format.DayNames);
            cip.DateTimeFormat.DateSeparator.Should().Be(format.DateSeparator);
            cip.DateTimeFormat.ShortDatePattern.Should().Be(format.ShortDatePattern);
            cip.DateTimeFormat.ShortestDayNames.Should().BeEquivalentTo(format.ShortestDayNames);
            cip.DateTimeFormat.ShortTimePattern.Should().Be(format.ShortTimePattern);
            cip.DateTimeFormat.YearMonthPattern.Should().Be(format.YearMonthPattern);
            cip.DateTimeFormat.TimeSeparator.Should().Be(format.TimeSeparator);
        }
    }
}