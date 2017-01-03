using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Widget;
using Flights.Core.ViewModels;
using MvvmCross.Droid.Views;

namespace Flights.Droid.Views
{
    [Activity(Label = "Flights", MainLauncher = true, Icon = "@drawable/Icon", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class MainPageView : MvxTabActivity
    {
        protected MainPageViewModel MainPageViewModel
        {
            get { return base.ViewModel as MainPageViewModel; }
        }

        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.MainPageView);
            
            TabHost.TabSpec spec;
            Intent intent;

            spec = TabHost.NewTabSpec("MainPageEntryViewModel");
            spec.SetIndicator(Resources.GetString(Resource.String.entry));
            spec.SetContent(this.CreateIntentFor(MainPageViewModel.MainPageEntryViewModel));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("MainPageFavoritesViewModel");
            spec.SetIndicator(Resources.GetString(Resource.String.favorites));
            spec.SetContent(this.CreateIntentFor(MainPageViewModel.MainPageFavoritesViewModel));
            TabHost.AddTab(spec);
        }
    }
}