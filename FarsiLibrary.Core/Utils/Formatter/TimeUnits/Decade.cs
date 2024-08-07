namespace FarsiLibrary.Core.Utils.Formatter.TimeUnits;

public class Decade : AbstractTimeUnit
{
    public Decade()
    {
        this.MillisPerUnit = 2629743830L * 12L * 10;
        this.Format.RoundingTolerance = 10000;
    }

    protected override string GetResourcePrefix()
    {
        return "Decade";
    }
}