using System;
using MvvmCross.Platform.Converters;
using System.Globalization;

namespace Flights.Droid.Converters
{
    public class BoolToFavoritePictureValueConverter : MvxValueConverter<bool, int>
    {
        protected override int Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value as bool?;
            if (boolValue == null)
            {
                throw new ArgumentException("BoolToPicture work only with bool type");
            }
            return ((bool)boolValue) ? Resource.Drawable.InFavorite : Resource.Drawable.NotInFavorite;
        }
    }
}