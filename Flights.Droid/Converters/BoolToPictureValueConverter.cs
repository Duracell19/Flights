using MvvmCross.Platform.Converters;
using System.Globalization;
using System;

namespace Flights.Droid.Converters
{
    public class BoolToPictureValueConverter : MvxValueConverter<bool, int>
    {
        protected override int Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value as bool?;
            if (boolValue == null)
            {
                throw new ArgumentException("BoolToPicture work only with bool type");
            }
            return ((bool)boolValue) ? Resource.Drawable.fly_return : Resource.Drawable.fly;
        }
    }
}