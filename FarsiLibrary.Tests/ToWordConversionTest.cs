namespace FarsiLibrary.Tests;

using System;

using FarsiLibrary.Core.Utils;

public class ToWordConversionTest
{
    [Fact]
    public void Should_Convert_Maximum_Integer_Values()
    {
        ToWords.ToString(int.MaxValue);
    }

    [Fact]
    public void Should_Convert_Big_Integer_Values()
    {
        var s = ToWords.ToString(int.MaxValue);
        Assert.NotNull(s);
    }

    [Fact]
    public void Should_Not_Be_Able_To_Convert_Larger_Than_Long_Values()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ToWords.ToString(long.MaxValue));
    }

    [Fact]
    public void Should_Not_Be_Able_To_Convert_Minus_Values()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => ToWords.ToString(-1000));
    }

    [Fact]
    public void Can_Convert_Singles()
    {
        var s = ToWords.ToString(8);
        Assert.NotNull(s);
    }

    [Theory]
    [InlineData("سی و يک", 31)]
    [InlineData("چهل و دو", 42)]
    [InlineData("پنجاه و سه", 53)]
    [InlineData("شصت و چهار", 64)]
    [InlineData("هفتاد و پنج", 75)]
    [InlineData("هشتاد و شش", 86)]
    [InlineData("نود و هفت", 97)]
    public void Can_Convert_Tens(string converted, int toConvert)
    {
        Assert.Equal(converted, ToWords.ToString(toConvert));
    }

    [Theory]
    [InlineData("صد و ده", 110)]
    [InlineData("دویست و بيست و يک", 221)]
    [InlineData("سیصد و سی و دو", 332)]
    [InlineData("چهارصد و چهل و سه", 443)]
    [InlineData("پانصد و پنجاه و چهار", 554)]
    [InlineData("ششصد و شصت و پنج", 665)]
    [InlineData("هفتصد و هفتاد و شش", 776)]
    [InlineData("هشتصد و هشتاد و هفت", 887)]
    [InlineData("نهصد و نود و هشت", 998)]
    public void Can_Convert_Hundreds(string converted, int toConvert)
    {
        Assert.Equal(converted, ToWords.ToString(toConvert));
    }

    [Theory]
    [InlineData("يک میلیارد", 1000000000)]
    [InlineData("يک میلیون", 1000000)]
    [InlineData("يک هزار", 1000)]
    [InlineData("صد", 100)]
    [InlineData("صفر", 0)]
    public void Can_Convert_Round_Numeric_Values(string converted, int toConvert)
    {
        Assert.Equal(converted, ToWords.ToString(toConvert));
    }

    [Fact]
    public void Can_Convert_Thousands()
    {
        var s = ToWords.ToString(1590);
        Assert.NotNull(s);
    }

    [Fact]
    public void Can_Convert_Ten_Thousands()
    {
        var s = ToWords.ToString(18910);
        Assert.NotNull(s);
    }

    [Fact]
    public void Can_Convert_Hundred_Thousands()
    {
        var s = ToWords.ToString(547230);
        Assert.NotNull(s);
    }

    [Fact]
    public void Can_Convert_Numeric_Characters_To_English()
    {
        var persianNumerals = "۰۱۲۳۴۵۶۷۸۹";
        var englishNumerals = toEnglish.Convert(persianNumerals);

        Assert.Equal("0123456789", englishNumerals);
    }

    [Fact]
    public void Can_Convert_Numeric_Characters_To_Persian()
    {
        var englishNumerals = "0123456789";
        var persianNumerals = toFarsi.Convert(englishNumerals);

        Assert.Equal("۰۱۲۳۴۵۶۷۸۹", persianNumerals);
    }

    [Fact]
    public void Can_Convert_Mixed_Strings_To_English_Numerals()
    {
        var persianNumerals = "۱۲۳ABC";
        var result = toEnglish.Convert(persianNumerals);

        Assert.Equal("123ABC", result);
    }

    [Fact]
    public void Can_Convert_Mixed_Strings_To_Persian_Numerals()
    {
        var persianNumerals = "123ABC";
        var result = toFarsi.Convert(persianNumerals);

        Assert.Equal("۱۲۳ABC", result);
    }
}