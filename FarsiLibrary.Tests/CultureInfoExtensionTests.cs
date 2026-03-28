namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils;
using FarsiLibrary.Core.Utils.Internals;

public class CultureInfoExtensionTests
{
    [Test]
    public void Can_Determine_Farsi_Culture()
    {
        new CultureInfo("fa-ir").IsFarsiCulture().Should().BeTrue();
        new PersianCultureInfo().IsFarsiCulture().Should().BeTrue();
        new CultureInfo("fa").IsFarsiCulture().Should().BeTrue();
        new CultureInfo("fr").IsFarsiCulture().Should().BeFalse();
    }

    [Test]
    public void Can_Determine_Arabic_Culture()
    {
        new CultureInfo("ar").IsArabicCulture().Should().BeTrue();
        new CultureInfo("ar-sa").IsArabicCulture().Should().BeTrue();
        new CultureInfo("fa-ir").IsArabicCulture().Should().BeFalse();
    }
}