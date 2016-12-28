using Android.App;
using Android.OS;
using Acr.UserDialogs;
using MvvmCross.Droid.Views;

namespace Flights.Droid.Views
{
    [Activity(Label = "Favorites")]
    public class MainPageFavoritesView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainPageFavoritesView);

            UserDialogs.Init(this);
        }
    }
}