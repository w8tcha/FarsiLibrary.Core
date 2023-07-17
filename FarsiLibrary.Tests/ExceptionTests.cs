namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils.Exceptions;

public class ExceptionTests
{
    [Fact]
    public void Can_Create_InvalidPersianDate_Exception()
    {
        var ex = new InvalidPersianDateException();
            
        Assert.Null(ex.InvalidValue);
        Assert.Empty(ex.Message);
    }

    [Fact]
    public void Can_Create_InvalidPersianDateFormat_Exception()
    {
        var ex = new InvalidPersianDateFormatException();
        Assert.Empty(ex.Message);
    }

    [Fact]
    public void Can_Create_FormatExceptionWithMessage()
    {
        var ex = new InvalidPersianDateFormatException("Invalid date format");

        Assert.NotNull(ex.Message);
        Assert.Equal("Invalid date format", ex.Message);
    }
}