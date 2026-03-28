namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils.Exceptions;

public class ExceptionTests
{
    [Test]
    public void Can_Create_InvalidPersianDate_Exception()
    {
        var ex = new InvalidPersianDateException();

        ex.InvalidValue.Should().BeNull();
        ex.Message.Should().BeEmpty();
    }

    [Test]
    public void Can_Create_InvalidPersianDateFormat_Exception()
    {
        var ex = new InvalidPersianDateFormatException();
        ex.Message.Should().BeEmpty();
    }

    [Test]
    public void Can_Create_FormatExceptionWithMessage()
    {
        var ex = new InvalidPersianDateFormatException("Invalid date format");

        ex.Message.Should().NotBeNull();
        ex.Message.Should().Be("Invalid date format");
    }
}