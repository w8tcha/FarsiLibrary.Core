namespace FarsiLibrary.Tests.Helpers;

public class DateTimeFormatWrapper
{
    public static DateTimeFormatInfo GetFormatInfo()
    {
        return new DateTimeFormatInfo
                   {
                       DateSeparator = "-",
                   };
    }
}