using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Flights.Droid.Converters
{
    public class OppositeBoolToVisibilityValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "visible" : "invisible";
        }
    }
}