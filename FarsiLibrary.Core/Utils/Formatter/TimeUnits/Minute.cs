namespace FarsiLibrary.Core.Utils.Formatter.TimeUnits;

public class Minute : AbstractTimeUnit
{
    public Minute()
    {
        this.MillisPerUnit = 1000L * 60L;
    }

    protected override string GetResourcePrefix()
    {
        return "Minute";
    }
}