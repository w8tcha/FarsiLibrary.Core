namespace FarsiLibrary.Tests;

using System;

public class PersianDateExtensionTests
{
    [Fact]
    public void Can_Get_End_Of_Week()
    {
        var today = new PersianDate(1388, 4, 25);
        var weekend = today.EndOfWeek();

        Assert.Equal((int)DayOfWeek.Friday, (int)weekend.DayOfWeek);
        Assert.Equal(1388, weekend.Year);
        Assert.Equal(4,weekend.Month);
        Assert.Equal(26,weekend.Day);
    }

    [Fact]
    public void Can_Get_Start_Of_Week()
    {
        var today = new PersianDate(1388, 4, 25);
        var weekend = today.StartOfWeek();

        Assert.Equal((int)DayOfWeek.Saturday, (int)weekend.DayOfWeek);
        Assert.Equal(1388, weekend.Year);
        Assert.Equal(4,weekend.Month);
        Assert.Equal(20,weekend.Day);            
    }

    [Fact]
    public void Can_Get_End_Of_Month()
    {
        var today = new PersianDate(1388, 4, 20);
        var weekend = today.EndOfMonth();

        Assert.Equal((int)DayOfWeek.Wednesday, (int)weekend.DayOfWeek);
        Assert.Equal(1388,weekend.Year);
        Assert.Equal(4,weekend.Month);
        Assert.Equal(31,weekend.Day);
    }

    [Fact]
    public void Can_Get_Start_Of_Month()
    {
        var today = new PersianDate(1388, 4, 20);
        var weekend = today.StartOfMonth();

        Assert.Equal((int)DayOfWeek.Monday, (int)weekend.DayOfWeek);
        Assert.Equal(1388,weekend.Year);
        Assert.Equal(4,weekend.Month);
        Assert.Equal(1,weekend.Day);
    }

    [Fact]
    public void Can_Combine_Date_And_Time_Parts()
    {
        var prevDate = new PersianDate(1380, 1, 1, 5, 22, 30);
        var today = new PersianDate(1388, 4, 25);
        var combined = today.Combine(prevDate);

        Assert.Equal(1388,combined.Year);
        Assert.Equal(4,combined.Month);
        Assert.Equal(25,combined.Day);
        Assert.Equal(5,combined.Hour);
        Assert.Equal(22,combined.Minute);
        Assert.Equal(30,combined.Second);
    }

    [Fact]
    public void Can_Get_End_Of_Week_For_Every_Day_Of_The_Week()
    {
        var firstDay = new PersianDate(1388, 7, 4);
        var secondDay = new PersianDate(1388, 7, 5);
        var thirdDay = new PersianDate(1388, 7, 6);
        var forthDay = new PersianDate(1388, 7, 7);
        var fifthDay = new PersianDate(1388, 7, 8);
        var sixthDay = new PersianDate(1388, 7, 9);
        var seventhDay = new PersianDate(1388, 7, 10);

        Assert.Equal(seventhDay,firstDay.EndOfWeek());
        Assert.Equal(seventhDay, secondDay.EndOfWeek());
        Assert.Equal(seventhDay, thirdDay.EndOfWeek());
        Assert.Equal(seventhDay, forthDay.EndOfWeek());
        Assert.Equal(seventhDay, fifthDay.EndOfWeek());
        Assert.Equal(seventhDay, sixthDay.EndOfWeek());
        Assert.Equal(seventhDay, seventhDay.EndOfWeek());
    }

    [Fact]
    public void Can_Get_Start_Of_Week_For_Every_Day_Of_The_Week()
    {
        var firstDay = new PersianDate(1388, 7, 4);
        var secondDay = new PersianDate(1388, 7, 5);
        var thirdDay = new PersianDate(1388, 7, 6);
        var forthDay = new PersianDate(1388, 7, 7);
        var fifthDay = new PersianDate(1388, 7, 8);
        var sixthDay = new PersianDate(1388, 7, 9);
        var seventhDay = new PersianDate(1388, 7, 10);

        Assert.Equal(firstDay, firstDay.StartOfWeek());
        Assert.Equal(firstDay, secondDay.StartOfWeek());
        Assert.Equal(firstDay, thirdDay.StartOfWeek());
        Assert.Equal(firstDay, forthDay.StartOfWeek());
        Assert.Equal(firstDay, fifthDay.StartOfWeek());
        Assert.Equal(firstDay, sixthDay.StartOfWeek());
        Assert.Equal(firstDay, seventhDay.StartOfWeek());            
    }
}