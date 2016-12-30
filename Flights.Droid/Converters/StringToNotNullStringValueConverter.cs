using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Flights.Droid.Converters
{
    public class StringToNotNullStringValueConverter : MvxValueConverter<string, string>
    {
        protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value) ? "No info" : value;
        }
    }
}