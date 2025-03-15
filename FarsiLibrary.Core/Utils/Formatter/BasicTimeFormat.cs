namespace FarsiLibrary.Core.Utils.Formatter;

using System;
using System.Globalization;

using FarsiLibrary.Core.Utils.Internals;

public class BasicTimeFormat : ITimeFormat
{
    public static string Negative = "-";
    public static string Sign = "%s";
    public static string Quantity = "%n";
    public static string Unit = "%u";

    public double RoundingTolerance { get;set; } = 0;
    public string Pattern { get;set; } = string.Empty;
    public string FuturePrefix { get; set; } = string.Empty;
    public string FutureSuffix { get;set; } = string.Empty;
    public string PastPrefix { get; set; } = string.Empty;
    public string PastSuffix { get; set; } = string.Empty;

    public string Format(Duration duration)
    {
        var sign = GetSign(duration);
        var quantity = this.GetQuantity(duration);
        var unit = GetGramaticallyCorrectName(duration, quantity);
        var result = this.ApplyPattern(sign, unit, quantity);

        result = this.Decorate(sign, result);

        return result;
    }

    private string Decorate(string sign, string result)
    {
        if (sign == Negative)
        {
            result = this.PastPrefix + " " + result + " " + this.PastSuffix;
        }
        else
        {
            result = this.FuturePrefix + " " + result + " " + this.FutureSuffix;
        }

        return result.Trim();
    }

    private string ApplyPattern(string sign, string unit, double quantity)
    {
        var result = this.Pattern.Replace(Sign, sign);
        var number = FormatNumber(quantity);

        result = result.Replace(Quantity, number);
        result = result.Replace(Unit, unit);

        return result;
    }

    private static string FormatNumber(double quantity)
    {
        return CultureHelper.IsFarsiCulture() ? ToWords.ToString(quantity) : quantity.ToString(CultureInfo.InvariantCulture);
    }

    private double GetQuantity(Duration duration)
    {
        var quantity = Math.Abs(duration.Quantity);

        if (duration.Delta != 0)
        {
            var threshold = Math.Abs(duration.Delta / duration.Unit.MillisPerUnit * 100);
            if (threshold < this.RoundingTolerance)
            {
                quantity += 1;
            }
        }

        return Math.Truncate(quantity);
    }

    private static string GetGramaticallyCorrectName(Duration d, double quantity)
    {
        var result = d.Unit.Name;
        var value = Math.Abs(quantity);
        if (value is 0 or > 1)
        {
            result = d.Unit.PluralName;
        }

        return result;
    }

    private static string GetSign(Duration d)
    {
        return d.Quantity < 0 ? Negative : string.Empty;
    }

    public BasicTimeFormat SetPattern(string pattern)
    {
        this.Pattern = pattern;
        return this;
    }

    public BasicTimeFormat SetFuturePrefix(string futurePrefix)
    {
        this.FuturePrefix = futurePrefix.Trim();
        return this;
    }

    public BasicTimeFormat SetFutureSuffix(string futureSuffix)
    {
        this.FutureSuffix = futureSuffix.Trim();
        return this;
    }

    public BasicTimeFormat SetPastPrefix(string pastPrefix)
    {
        this.PastPrefix = pastPrefix.Trim();
        return this;
    }

    public BasicTimeFormat SetPastSuffix(string pastSuffix)
    {
        this.PastSuffix = pastSuffix.Trim();
        return this;
    }
}