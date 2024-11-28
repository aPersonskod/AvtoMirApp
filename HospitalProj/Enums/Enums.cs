using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using HospitalProj.Connection;

namespace HospitalProj.Enums;

public enum Sex
{
    [Description("Мужской")]
    M,
    [Description("Женский")]
    F
}

public enum PatientStatus
{
    [Description("Диагностическая")]
    Diag,
    [Description("Терапия")]
    Therapy,
    [Description("На паузе")]
    Paused,
    [Description("Закончили")]
    Ended
}

[ValueConversion(typeof(Enum), typeof(IEnumerable<ValueDescription>))]
public class EnumToCollectionConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return EnumHelper.GetAllValuesAndDescriptions(value.GetType());
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}