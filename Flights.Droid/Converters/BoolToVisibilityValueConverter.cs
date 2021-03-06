using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Flights.Droid.Converters
{
    public class BoolToVisibilityValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "invisible" : "visible";
        }
    }
}