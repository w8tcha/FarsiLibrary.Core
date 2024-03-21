namespace FarsiLibrary.Tests.Helpers;

public static class DateTimeFormatWrapper
{
    public static DateTimeFormatInfo GetFormatInfo()
    {
        return new DateTimeFormatInfo
                   {
                       DateSeparator = "-",
                   };
    }
}