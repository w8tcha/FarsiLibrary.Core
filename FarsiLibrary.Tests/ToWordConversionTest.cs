namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;

using System;

public class ToWordConversionTest
{
    [Test]
    public void Should_Convert_Maximum_Integer_Values()
    {
        ToWords.ToString(int.MaxValue);
    }

    [Test]
    public void Should_Convert_Big_Integer_Values()
    {
        var s = ToWords.ToString(int.MaxValue);
        s.Should().NotBeNull();
    }

    [Test]
    public void Should_Not_Be_Able_To_Convert_Larger_Than_Long_Values()
    {
        new Action(() => { ToWords.ToString(long.MaxValue); }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Should_Not_Be_Able_To_Convert_Minus_Values()
    {
        new Action(() => { ToWords.ToString(-1000); }).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Can_Convert_Singles()
    {
        var s = ToWords.ToString(8);
        s.Should().NotBeNull();
    }

    [TestCase("سی و يک", 31)]
    [TestCase("چهل و دو", 42)]
    [TestCase("پنجاه و سه", 53)]
    [TestCase("شصت و چهار", 64)]
    [TestCase("هفتاد و پنج", 75)]
    [TestCase("هشتاد و شش", 86)]
    [TestCase("نود و هفت", 97)]
    public void Can_Convert_Tens(string converted, int toConvert)
    {
        ToWords.ToString(toConvert).Should().Be(converted);
    }

    [TestCase("صد و ده", 110)]
    [TestCase("دویست و بيست و يک", 221)]
    [TestCase("سیصد و سی و دو", 332)]
    [TestCase("چهارصد و چهل و سه", 443)]
    [TestCase("پانصد و پنجاه و چهار", 554)]
    [TestCase("ششصد و شصت و پنج", 665)]
    [TestCase("هفتصد و هفتاد و شش", 776)]
    [TestCase("هشتصد و هشتاد و هفت", 887)]
    [TestCase("نهصد و نود و هشت", 998)]
    public void Can_Convert_Hundreds(string converted, int toConvert)
    {
        ToWords.ToString(toConvert).Should().Be(converted);
    }

    [Theory]
    [TestCase("يک میلیارد", 1000000000)]
    [TestCase("يک میلیون", 1000000)]
    [TestCase("يک هزار", 1000)]
    [TestCase("صد", 100)]
    [TestCase("صفر", 0)]
    public void Can_Convert_Round_Numeric_Values(string converted, int toConvert)
    {
        ToWords.ToString(toConvert).Should().Be(converted);
    }

    [Test]
    public void Can_Convert_Thousands()
    {
        var s = ToWords.ToString(1590);
        s.Should().NotBeNull();
    }

    [Test]
    public void Can_Convert_Ten_Thousands()
    {
        var s = ToWords.ToString(18910);
        s.Should().NotBeNull();
    }

    [Test]
    public void Can_Convert_Hundred_Thousands()
    {
        var s = ToWords.ToString(547230);
        s.Should().NotBeNull();
    }

    [Test]
    public void Can_Convert_Numeric_Characters_To_English()
    {
        var persianNumerals = "۰۱۲۳۴۵۶۷۸۹";
        var englishNumerals = toEnglish.Convert(persianNumerals);

        englishNumerals.Should().Be("0123456789");
    }

    [Test]
    public void Can_Convert_Numeric_Characters_To_Persian()
    {
        var englishNumerals = "0123456789";
        var persianNumerals = toFarsi.Convert(englishNumerals);

        persianNumerals.Should().Be("۰۱۲۳۴۵۶۷۸۹");
    }

    [Test]
    public void Can_Convert_Mixed_Strings_To_English_Numerals()
    {
        var persianNumerals = "۱۲۳ABC";
        var result = toEnglish.Convert(persianNumerals);

        result.Should().Be("123ABC");
    }

    [Test]
    public void Can_Convert_Mixed_Strings_To_Persian_Numerals()
    {
        var persianNumerals = "123ABC";
        var result = toFarsi.Convert(persianNumerals);

        result.Should().Be("۱۲۳ABC");
    }
}