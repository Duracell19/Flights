using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Flights.Droid.Converters
{
    public class OppositeValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
    }
}