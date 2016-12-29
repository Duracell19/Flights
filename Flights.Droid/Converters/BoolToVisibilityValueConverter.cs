using System;
using MvvmCross.Platform.Converters;
using MvvmCross.Platform.UI;
using System.Globalization;

namespace Flights.Droid.Converters
{
    public class BoolToVisibilityValueConverter : MvxValueConverter<bool, MvxVisibility>
    {
        protected override MvxVisibility Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? MvxVisibility.Collapsed : MvxVisibility.Visible;
        }
    }
}