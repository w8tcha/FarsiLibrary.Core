namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;

public class PersianDateExtensionTests
{
    [Test]
    public void Can_Get_End_Of_Week()
    {
        var today = new PersianDate(1388, 4, 25);
        var weekend = today.EndOfWeek();

        ((int)weekend.DayOfWeek).Should().Be((int)DayOfWeek.Friday);
        weekend.Year.Should().Be(1388);
        weekend.Month.Should().Be(4);
        weekend.Day.Should().Be(26);
    }

    [Test]
    public void Can_Get_Start_Of_Week()
    {
        var today = new PersianDate(1388, 4, 25);
        var weekend = today.StartOfWeek();

        ((int)weekend.DayOfWeek).Should().Be((int)DayOfWeek.Saturday);
        weekend.Year.Should().Be(1388);
        weekend.Month.Should().Be(4);
        weekend.Day.Should().Be(20);
    }

    [Test]
    public void Can_Get_End_Of_Month()
    {
        var today = new PersianDate(1388, 4, 20);
        var weekend = today.EndOfMonth();

        ((int)weekend.DayOfWeek).Should().Be((int)DayOfWeek.Wednesday);
        weekend.Year.Should().Be(1388);
        weekend.Month.Should().Be(4);
        weekend.Day.Should().Be(31);
    }

    [Test]
    public void Can_Get_Start_Of_Month()
    {
        var today = new PersianDate(1388, 4, 20);
        var weekend = today.StartOfMonth();

        ((int)weekend.DayOfWeek).Should().Be((int)DayOfWeek.Monday);
        weekend.Year.Should().Be(1388);
        weekend.Month.Should().Be(4);
        weekend.Day.Should().Be(1);
    }

    [Test]
    public void Can_Combine_Date_And_Time_Parts()
    {
        var prevDate = new PersianDate(1380, 1, 1, 5, 22, 30);
        var today = new PersianDate(1388, 4, 25);
        var combined = today.Combine(prevDate);

        combined.Year.Should().Be(1388);
        combined.Month.Should().Be(4);
        combined.Day.Should().Be(25);
        combined.Hour.Should().Be(5);
        combined.Minute.Should().Be(22);
        combined.Second.Should().Be(30);
    }

    [Test]
    public void Can_Get_End_Of_Week_For_Every_Day_Of_The_Week()
    {
        var firstDay = new PersianDate(1388, 7, 4);
        var secondDay = new PersianDate(1388, 7, 5);
        var thirdDay = new PersianDate(1388, 7, 6);
        var forthDay = new PersianDate(1388, 7, 7);
        var fifthDay = new PersianDate(1388, 7, 8);
        var sixthDay = new PersianDate(1388, 7, 9);
        var seventhDay = new PersianDate(1388, 7, 10);

        firstDay.EndOfWeek().Should().Be(seventhDay);
        secondDay.EndOfWeek().Should().Be(seventhDay);
        thirdDay.EndOfWeek().Should().Be(seventhDay);
        forthDay.EndOfWeek().Should().Be(seventhDay);
        fifthDay.EndOfWeek().Should().Be(seventhDay);
        sixthDay.EndOfWeek().Should().Be(seventhDay);
        seventhDay.EndOfWeek().Should().Be(seventhDay);
    }

    [Test]
    public void Can_Get_Start_Of_Week_For_Every_Day_Of_The_Week()
    {
        var firstDay = new PersianDate(1388, 7, 4);
        var secondDay = new PersianDate(1388, 7, 5);
        var thirdDay = new PersianDate(1388, 7, 6);
        var forthDay = new PersianDate(1388, 7, 7);
        var fifthDay = new PersianDate(1388, 7, 8);
        var sixthDay = new PersianDate(1388, 7, 9);
        var seventhDay = new PersianDate(1388, 7, 10);

        firstDay.StartOfWeek().Should().Be(firstDay);
        secondDay.StartOfWeek().Should().Be(firstDay);
        thirdDay.StartOfWeek().Should().Be(firstDay);
        forthDay.StartOfWeek().Should().Be(firstDay);
        fifthDay.StartOfWeek().Should().Be(firstDay);
        sixthDay.StartOfWeek().Should().Be(firstDay);
        seventhDay.StartOfWeek().Should().Be(firstDay);
    }
}