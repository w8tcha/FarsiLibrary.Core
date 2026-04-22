namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Internals;
using FarsiLibrary.Tests.Helpers;

using System;

public class InternalTests
{
    [Test]
    public void Can_Get_IndexOfDay_For_PersianCalendar_Using_CultureHelper()
    {
        var pc = new PersianCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, pc);

        dow.Should().Be(2);
    }

    [Test]
    public void Can_Get_IndexOfDay_For_HijriCalendar_Using_CultureHelper()
    {
        var hc = new HijriCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, hc);

        dow.Should().Be(1);
    }

    [Test]
    public void Can_Get_IndexOfDay_For_Other_Calendars_Using_CultureHelper()
    {
        var calendar = new GregorianCalendar();
        var dt = new DateTime(2009, 5, 11); // Should be Monday
        var dow = CultureHelper.GetDayOfWeek(dt, calendar);

        dow.Should().Be(1);
    }

    [Test]
    public void Can_Get_Correct_Current_Calendar()
    {
        using (new CultureSwitchContext(new PersianCultureInfo()))
        {
            var calendar = CultureHelper.CurrentCalendar;
            calendar.Should().BeOfType<PersianCalendar>();
        }

        using (new CultureSwitchContext(new CultureInfo("ar-sa")))
        {
            var calendar = CultureHelper.CurrentCalendar;
            calendar.Should().BeOfType<HijriCalendar>();
        }

        using (new CultureSwitchContext(new CultureInfo("en-us")))
        {
            var calendar = CultureHelper.CurrentCalendar;
            calendar.Should().BeOfType<GregorianCalendar>();
        }
    }

    [Test]
    public void Can_Get_Correct_DayOfWeek_Using_CultureHelper()
    {
        using(new CultureSwitchContext(new PersianCultureInfo()))
        {
            var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); // It is a zero based index
            dow.Should().Be(DayOfWeek.Monday);
        }

        using(new CultureSwitchContext(new CultureInfo("en-us")))
        {
            var dow = CultureHelper.GetCultureDayOfWeek(2, CultureHelper.CurrentCulture); // It is a zero based index
            dow.Should().Be(DayOfWeek.Tuesday);
        }
    }

    [Test]
    public void Max_Min_Supported_Value_Equals_PersianDate_Max_Min_Values()
    {
        using(new CultureSwitchContext(new CultureInfo("fa-ir")))
        {
            var min = CultureHelper.MinCultureDateTime;
            var max = CultureHelper.MaxCultureDateTime;

            min.Should().Be(PersianDate.MinValue);
            max.Should().Be(PersianDate.MaxValue);
        }
    }

    [Test]
    public void Can_Guard_Against_Wrong_Conditions()
    {
        new Action(() => { Guard.Against(true, "Value Wrong"); }).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public void Can_Guard_With_Specific_Exception()
    {
        new Action(() => { Guard.Against<OutOfMemoryException>(true, "Out Of memory"); }).Should().Throw<OutOfMemoryException>();
    }

    [Test]
    public void Will_Not_Throw_If_Condition_Is_Not_Met()
    {
        var exception = () => { Guard.Against(false, string.Empty); };
        exception.Should().NotThrow();

        exception = () => { Guard.Against<Exception>(false, string.Empty); };
        exception.Should().NotThrow();
    }

    [Test]
    public void Can_Get_Field_Value()
    {
        var test = new ReflectionTestClass();
        var value = ReflectionHelper.GetField<string>(test, "testField");

        value.Should().Be("TestValue");
    }

    [Test]
    public void Can_Set_Field_Value()
    {
        var test = new ReflectionTestClass();
        ReflectionHelper.SetField(test, "testField" , "NewValue");
        var value = ReflectionHelper.GetField<string>(test, "testField");

        value.Should().Be("NewValue");
    }

    [Test]
    public void Getting_Field_Value_With_Null_Owner_Throws()
    {
        new Action(() => { ReflectionHelper.GetField((object)null, "testField"); }).Should().Throw<ArgumentNullException>();
        new Action(() => { ReflectionHelper.GetField(new ReflectionTestClass(), "NonExistingField"); }).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Getting_Property_Value_With_Null_Owner_Throws()
    {
        new Action(() => { ReflectionHelper.GetProperty(null, "testField"); }).Should().Throw<ArgumentNullException>();
        new Action(() => { ReflectionHelper.GetProperty(new ReflectionTestClass(), "NonExistingField"); }).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Setting_Field_Value_With_Null_Owner_Throws()
    {
        new Action(() => { ReflectionHelper.SetField(null, "testField", "TestValue"); }).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Can_Get_Property_Value()
    {
        var test = new ReflectionTestClass();
        var value = ReflectionHelper.GetProperty<string>(test, "TestProperty");

        value.Should().Be("TestValue");
    }

    [Test]
    public void Can_Invoke_Method()
    {
        var test = new ReflectionTestClass();
        ReflectionHelper.InvokeMethod(test, "Method");

        (test.MethodInvoked).Should().BeTrue();
    }

    [Test]
    public void Can_Invoke_StaticMethod()
    {
        ReflectionHelper.InvokeStaticMethod(typeof(ReflectionTestClass), "StaticMethod");

        (ReflectionTestClass.StaticMethodInvoked).Should().BeTrue();
    }

    public class ReflectionTestClass
    {
        public ReflectionTestClass()
        {
            this.testField = this.TestProperty = "TestValue";
        }

        private readonly string testField;

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