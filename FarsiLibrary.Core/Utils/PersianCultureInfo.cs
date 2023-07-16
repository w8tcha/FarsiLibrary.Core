namespace FarsiLibrary.Core.Utils;

using System;
using System.Globalization;

using FarsiLibrary.Core.Utils.Internals;

using DateTimeFormatInfo=System.Globalization.DateTimeFormatInfo;

/// <summary>
/// CultureInfo for "FA-IR" culture, which has correct calendar information.
/// </summary>
public class PersianCultureInfo : CultureInfo
{
    private struct CultureFieldNames
    {
        public static string Calendar => "calendar";

        public static string IsReadonly => "m_isReadOnly";
    }

    //private static CultureFieldNames FieldNames;
    private readonly PersianCalendar calendar;
    private readonly System.Globalization.PersianCalendar systemCalendar;
    private DateTimeFormatInfo format;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersianCultureInfo"/> class.
    /// </summary>
    public PersianCultureInfo()
        : base("fa-IR", false)
    {
        this.calendar = new PersianCalendar();
        this.systemCalendar = new System.Globalization.PersianCalendar();
        this.format = this.CreateDateTimeFormatInfo();
        this.SetCalendar();
    }

    protected void SetCalendar()
    {
        ReflectionHelper.SetField(this.format, CultureFieldNames.Calendar, this.systemCalendar);
        base.DateTimeFormat = this.format;
    }

    protected internal DateTimeFormatInfo CreateDateTimeFormatInfo()
    {
        return this.format ??= new DateTimeFormatInfo
                                   {
                                       AbbreviatedDayNames =
                                           PersianDateTimeFormatInfo.AbbreviatedDayNames,
                                       AbbreviatedMonthGenitiveNames =
                                           PersianDateTimeFormatInfo.AbbreviatedMonthGenitiveNames,
                                       AbbreviatedMonthNames =
                                           PersianDateTimeFormatInfo.AbbreviatedMonthNames,
                                       AMDesignator = PersianDateTimeFormatInfo.AMDesignator,
                                       DateSeparator = PersianDateTimeFormatInfo.DateSeparator,
                                       DayNames = PersianDateTimeFormatInfo.DayNames,
                                       FirstDayOfWeek = PersianDateTimeFormatInfo.FirstDayOfWeek,
                                       FullDateTimePattern =
                                           PersianDateTimeFormatInfo.FullDateTimePattern,
                                       LongDatePattern = PersianDateTimeFormatInfo.LongDatePattern,
                                       LongTimePattern = PersianDateTimeFormatInfo.LongTimePattern,
                                       MonthDayPattern = PersianDateTimeFormatInfo.MonthDayPattern,
                                       MonthGenitiveNames = PersianDateTimeFormatInfo.MonthGenitiveNames,
                                       MonthNames = PersianDateTimeFormatInfo.MonthNames,
                                       PMDesignator = PersianDateTimeFormatInfo.PMDesignator,
                                       ShortDatePattern = PersianDateTimeFormatInfo.ShortDatePattern,
                                       ShortestDayNames = PersianDateTimeFormatInfo.ShortestDayNames,
                                       ShortTimePattern = PersianDateTimeFormatInfo.ShortTimePattern,
                                       TimeSeparator = PersianDateTimeFormatInfo.TimeSeparator,
                                       YearMonthPattern = PersianDateTimeFormatInfo.YearMonthPattern
                                   };
        // Make format information readonly to fix
        // cloning problems that might happen with 
        // other controls.
        //ReflectionHelper.SetField(this.format, FieldNames.IsReadonly, true);
    }

    /// <summary>
    /// Gets the default calendar used by the culture.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Globalization.Calendar"/> that represents the default calendar used by the culture.
    /// </returns>
    public override Calendar Calendar => this.systemCalendar;

    /// <summary>
    /// Gets the list of calendars that can be used by the culture.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An array of type <see cref="T:System.Globalization.Calendar"/> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo"/>.
    /// </returns>
    public override Calendar[] OptionalCalendars
    {
        get { return new Calendar[] { this.systemCalendar, this.calendar }; }
    }
        

    /// <summary>
    /// Creates a copy of the current <see cref="T:System.Globalization.CultureInfo"/>.
    /// </summary>
    /// <returns>
    /// A copy of the current <see cref="T:System.Globalization.CultureInfo"/>.
    /// </returns>
    public override object Clone()
    {
        return new PersianCultureInfo();
    }

    public static new bool IsReadOnly => true;

    public override bool IsNeutralCulture => false;

    /// <summary>
    /// Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo"/> that defines the culturally appropriate format of displaying dates and times.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Globalization.DateTimeFormatInfo"/> that defines the culturally appropriate format of displaying dates and times.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// The property is set to null.
    /// </exception>
    public override DateTimeFormatInfo DateTimeFormat
    {
        get => this.format;
        set => this.format = value ?? throw new ArgumentNullException(nameof(value), "value can not be null.");
    }
}