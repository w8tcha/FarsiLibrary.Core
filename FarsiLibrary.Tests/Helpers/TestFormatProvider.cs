namespace FarsiLibrary.Tests.Helpers;

using System;

using FarsiLibrary.Core.Utils;

public class TestFormatProvider : IFormatProvider
{
    public object GetFormat(Type formatType)
    {
        return new CustomFormatter();
    }

    private class CustomFormatter : ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var pd = arg as PersianDate;

            if (pd == null)
            {
                return pd.ToString(format);
            }

            if (format == "CustomYearMonth")
                return pd.Year + " -- " + pd.Month;

            return pd.ToString(format);
        }
    }
}