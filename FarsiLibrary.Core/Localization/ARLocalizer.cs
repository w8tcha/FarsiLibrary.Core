namespace FarsiLibrary.Core.Localization;

/// <summary>
/// Localizer class used to get string values of Arabic language.
/// </summary>
public class ARLocalizer : FALocalizer
{
    /// <summary>
    /// Gets a localized string for Arabic culture, for specified <see cref="StringID"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public override string GetLocalizedString(StringID id)
    {
        return id switch
            {
                StringID.Empty => string.Empty,
                StringID.Numbers_0 => "٠",
                StringID.Numbers_1 => "١",
                StringID.Numbers_2 => "٢",
                StringID.Numbers_3 => "٣",
                StringID.Numbers_4 => "٤",
                StringID.Numbers_5 => "٥",
                StringID.Numbers_6 => "٦",
                StringID.Numbers_7 => "٧",
                StringID.Numbers_8 => "٨",
                StringID.Numbers_9 => "٩",
                StringID.FADateTextBox_Required => "هذا الحقل إلزامي",
                StringID.FAMonthView_None => "فارغ",
                StringID.FAMonthView_Today => "اليوم",
                StringID.PersianDate_InvalidDateFormat => "تنسيق التاريخ غير صالح.",
                StringID.PersianDate_InvalidDateTime => "قيمة التاريخ/الوقت غير صالحة.",
                StringID.PersianDate_InvalidDateTimeLength => "النص المدخل للتاريخ/الوقت غير صالح.",
                StringID.PersianDate_InvalidDay => "قيمة اليوم غير صالحة.",
                StringID.PersianDate_InvalidEra => "النطاق المدخل غير صالح.",
                StringID.PersianDate_InvalidFourDigitYear => "لا يمكن تحويل القيمة المدخلة إلى سنة.",
                StringID.PersianDate_InvalidHour => "قيمة الساعة غير صالحة.",
                StringID.PersianDate_InvalidLeapYear => "هذه السنة ليست كبيسة. صحح قيمة اليوم.",
                StringID.PersianDate_InvalidMillisecond => "قيمة الميلي ثانية غير صالحة.",
                StringID.PersianDate_InvalidMinute => "قيمة الدقيقة غير صالحة.",
                StringID.PersianDate_InvalidMonth => "قيمة الشهر غير صالحة.",
                StringID.PersianDate_InvalidMonthDay => "قيمة الشهر/اليوم غير صالحة.",
                StringID.PersianDate_InvalidSecond => "قيمة الثانية غير صالحة.",
                StringID.PersianDate_InvalidTimeFormat => "تنسيق الوقت غير صالح.",
                StringID.PersianDate_InvalidYear => "قيمة السنة غير صالحة.",
                StringID.Validation_Cancel => "إلغاء",
                StringID.Validation_NotValid => "القيمة المحددة خارج النطاق المسموح به.",
                StringID.Validation_Required => "حقل إلزامي. الرجاء إدخال قيمة.",
                StringID.Validation_NullText => "[لا توجد قيمة]",
                StringID.MessageBox_Ok => "موافق",
                StringID.MessageBox_Cancel => "إلغاء",
                StringID.MessageBox_Abort => "إلغاء",
                StringID.MessageBox_Ignore => "تجاهل",
                StringID.MessageBox_Retry => "إعادة المحاولة",
                StringID.MessageBox_No => "لا",
                StringID.MessageBox_Yes => "نعم",
                StringID.Hour => "ساعة",
                StringID.Minute => "دقيقة",
                StringID.Second => "ثانية",
                _ => string.Empty
            };
    }

    /// <summary>
    /// Gets a localized formatter string for Arabic culture, for specified <see cref="FormatterStringID"/>.
    /// </summary>
    /// <param name="stringID"></param>
    /// <returns></returns>
    public override string GetFormatterString(FormatterStringID stringID)
    {
        return stringID switch
            {
                FormatterStringID.CenturyPattern => "%n %u",
                FormatterStringID.CenturyFuturePrefix => string.Empty,
                FormatterStringID.CenturyFutureSuffix => " بعد",
                FormatterStringID.CenturyPastPrefix => string.Empty,
                FormatterStringID.CenturyPastSuffix => " قبل",
                FormatterStringID.CenturyName => "قرن",
                FormatterStringID.CenturyPluralName => "قرون",
                FormatterStringID.DayPattern => "%n %u",
                FormatterStringID.DayFuturePrefix => string.Empty,
                FormatterStringID.DayFutureSuffix => " بعد",
                FormatterStringID.DayPastPrefix => string.Empty,
                FormatterStringID.DayPastSuffix => " قبل",
                FormatterStringID.DayName => "يوم",
                FormatterStringID.DayPluralName => "أيام",
                FormatterStringID.DecadePattern => "%n %u",
                FormatterStringID.DecadeFuturePrefix => string.Empty,
                FormatterStringID.DecadeFutureSuffix => " بعد",
                FormatterStringID.DecadePastPrefix => string.Empty,
                FormatterStringID.DecadePastSuffix => " قبل",
                FormatterStringID.DecadeName => "عقد",
                FormatterStringID.DecadePluralName => "عقود",
                FormatterStringID.HourPattern => "%n %u",
                FormatterStringID.HourFuturePrefix => string.Empty,
                FormatterStringID.HourFutureSuffix => " بعد",
                FormatterStringID.HourPastPrefix => string.Empty,
                FormatterStringID.HourPastSuffix => " قبل",
                FormatterStringID.HourName => "ساعة",
                FormatterStringID.HourPluralName => "ساعات",
                FormatterStringID.JustNowPattern => "%u",
                FormatterStringID.JustNowFuturePrefix => string.Empty,
                FormatterStringID.JustNowFutureSuffix => "لحظات قادمة",
                FormatterStringID.JustNowPastPrefix => "قبل لحظات",
                FormatterStringID.JustNowPastSuffix => string.Empty,
                FormatterStringID.JustNowName => string.Empty,
                FormatterStringID.JustNowPluralName => string.Empty,
                FormatterStringID.MillenniumPattern => "%n %u",
                FormatterStringID.MillenniumFuturePrefix => string.Empty,
                FormatterStringID.MillenniumFutureSuffix => " بعد",
                FormatterStringID.MillenniumPastPrefix => string.Empty,
                FormatterStringID.MillenniumPastSuffix => " قبل",
                FormatterStringID.MillenniumName => "ألفية",
                FormatterStringID.MillenniumPluralName => "ألفيات",
                FormatterStringID.MillisecondPattern => "%n %u",
                FormatterStringID.MillisecondFuturePrefix => string.Empty,
                FormatterStringID.MillisecondFutureSuffix => " بعد",
                FormatterStringID.MillisecondPastPrefix => string.Empty,
                FormatterStringID.MillisecondPastSuffix => " قبل",
                FormatterStringID.MillisecondName => "جزء من الثانية",
                FormatterStringID.MillisecondPluralName => "أجزاء من الثانية",
                FormatterStringID.MinutePattern => "%n %u",
                FormatterStringID.MinuteFuturePrefix => string.Empty,
                FormatterStringID.MinuteFutureSuffix => " بعد",
                FormatterStringID.MinutePastPrefix => string.Empty,
                FormatterStringID.MinutePastSuffix => " قبل",
                FormatterStringID.MinuteName => "دقيقة",
                FormatterStringID.MinutePluralName => "دقائق",
                FormatterStringID.MonthPattern => "%n %u",
                FormatterStringID.MonthFuturePrefix => string.Empty,
                FormatterStringID.MonthFutureSuffix => " بعد",
                FormatterStringID.MonthPastPrefix => string.Empty,
                FormatterStringID.MonthPastSuffix => " قبل",
                FormatterStringID.MonthName => "شهر",
                FormatterStringID.MonthPluralName => "أشهر",
                FormatterStringID.SecondPattern => "%n %u",
                FormatterStringID.SecondFuturePrefix => string.Empty,
                FormatterStringID.SecondFutureSuffix => " بعد",
                FormatterStringID.SecondPastPrefix => string.Empty,
                FormatterStringID.SecondPastSuffix => " قبل",
                FormatterStringID.SecondName => "ثانية",
                FormatterStringID.SecondPluralName => "ثواني",
                FormatterStringID.WeekPattern => "%n %u",
                FormatterStringID.WeekFuturePrefix => string.Empty,
                FormatterStringID.WeekFutureSuffix => " بعد",
                FormatterStringID.WeekPastPrefix => string.Empty,
                FormatterStringID.WeekPastSuffix => " قبل",
                FormatterStringID.WeekName => "أسبوع",
                FormatterStringID.WeekPluralName => "أسابيع",
                FormatterStringID.YearPattern => "%n %u",
                FormatterStringID.YearFuturePrefix => string.Empty,
                FormatterStringID.YearFutureSuffix => " بعد",
                FormatterStringID.YearPastPrefix => string.Empty,
                FormatterStringID.YearPastSuffix => " قبل",
                FormatterStringID.YearName => "سنة",
                FormatterStringID.YearPluralName => "سنوات",
                _ => string.Empty
            };
    }
}
