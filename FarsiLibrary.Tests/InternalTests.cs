namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Internals;
using FarsiLibrary.Tests.Helpers;

public class InternalTests
{
    [Fact]
    public void Can_Get_IndexOfDay_For_PersianCalendar_Using_CultureHelper()
    {
        var pc = new PersianCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, pc);

        Assert.Equal(2, dow);
    }

    [Fact]
    public void Can_Get_IndexOfDay_For_HijriCalendar_Using_CultureHelper()
    {
        var hc = new HijriCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, hc);

        Assert.Equal(1, dow);
    }

    [Fact]
    public void Can_Get_IndexOfDay_For_Other_Calendars_Using_CultureHelper()
    {
        var calendar = new GregorianCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, calendar);

        Assert.Equal(1, dow);
    }

    [Fact]
    public void Can_Get_Correct_Current_Calendar()
    {
        using(new CultureSwitchContext(new PersianCultureInfo()))
        {
            var calendar = CultureHelper.CurrentCalendar;
            Assert.IsType<PersianCalendar>(calendar);
        }

        using(new CultureSwitchContext(new CultureInfo("ar-sa")))
        {
            var calendar = CultureHelper.CurrentCalendar;
            Assert.IsType<HijriCalendar>(calendar);
        }

        using (new CultureSwitchContext(new CultureInfo("en-us")))
        {
            var calendar = CultureHelper.CurrentCalendar;
            Assert.IsType<GregorianCalendar>(calendar);
        }
    }

    [Fact]
    public void Can_Get_Correct_DayOfWeek_Using_CultureHelper()
    {
        using(new CultureSwitchContext(new PersianCultureInfo()))
        {
            var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); // It is a zero based index
            Assert.Equal(DayOfWeek.Monday, dow);
        }

        using(new CultureSwitchContext(new CultureInfo("en-us")))
        {
            var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); // It is a zero based index
            Assert.Equal(DayOfWeek.Tuesday, dow);
        }
    }

    [Fact]
    public void Max_Min_Supported_Value_Equals_PersianDate_Max_Min_Values()
    {
        using(new CultureSwitchContext(new CultureInfo("fa-ir")))
        {
            var min = CultureHelper.MinCultureDateTime;
            var max = CultureHelper.MaxCultureDateTime;

            Assert.Equal(PersianDate.MinValue, min);
            Assert.Equal(PersianDate.MaxValue, max);
        }
    }

    [Fact]
    public void Can_Guard_Against_Wrong_Conditions()
    {
        Assert.Throws<InvalidOperationException>(() => Guard.Against(true, "Value Wrong"));
    }

    [Fact]
    public void Can_Guard_With_Specific_Exception()
    {
        Assert.Throws<OutOfMemoryException>(() => Guard.Against<OutOfMemoryException>(true, "Out Of memory"));
    }

    [Fact]
    public void Will_Not_Throw_If_Condition_Is_Not_Met()
    {
        var exception = Record.Exception(() => Guard.Against(false, string.Empty));
        Assert.Null(exception);

        exception = Record.Exception(() => Guard.Against<Exception>(false, string.Empty));
        Assert.Null(exception);
    }

    [Fact]
    public void Can_Get_Field_Value()
    {
        var test = new ReflectionTestClass();
        var value = ReflectionHelper.GetField<string>(test, "TestField");

        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void Can_Set_Field_Value()
    {
        var test = new ReflectionTestClass();
        ReflectionHelper.SetField(test, "TestField" , "NewValue");
        var value = ReflectionHelper.GetField<string>(test, "TestField");

        Assert.Equal("NewValue", value);
    }

    [Fact]
    public void Getting_Field_Value_With_Null_Owner_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetField((object)null, "TestField"));
        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetField(new ReflectionTestClass(), "NonExistingField"));
    }

    [Fact]
    public void Getting_Property_Value_With_Null_Owner_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetProperty(null, "TestField"));
        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.GetProperty(new ReflectionTestClass(), "NonExistingField"));
    }

    [Fact]
    public void Setting_Field_Value_With_Null_Owner_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => ReflectionHelper.SetField(null, "TestField", "TestValue"));
    }

    [Fact]
    public void Can_Get_Property_Value()
    {
        var test = new ReflectionTestClass();
        var value = ReflectionHelper.GetProperty<string>(test, "TestProperty");

        Assert.Equal("TestValue", value);
    }

    [Fact]
    public void Can_Invoke_Method()
    {
        var test = new ReflectionTestClass();
        ReflectionHelper.InvokeMethod(test, "Method");

        Assert.True(test.MethodInvoked);
    }

    [Fact]
    public void Can_Invoke_StaticMethod()
    {
        ReflectionHelper.InvokeStaticMethod(typeof(ReflectionTestClass), "StaticMethod");

        Assert.True(ReflectionTestClass.StaticMethodInvoked);
    }

    public class ReflectionTestClass
    {
        public ReflectionTestClass()
        {
            this.TestField = this.TestProperty = "TestValue";
        }

        private string TestField;

        public string TestProperty
        {
            get; set;
        }

        public static bool StaticMethodInvoked;

        public bool MethodInvoked;

        private void Method()
        {
            this.MethodInvoked = true;
        }

        private static void StaticMethod()
        {
            StaticMethodInvoked = true;
        }
    }
}