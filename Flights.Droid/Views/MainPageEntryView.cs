using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace Flights.Droid.Views
{
    [Activity(Label = "Entry")]
    public class MainPageEntryView : MvxActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainPageEntryView);
        }
    }
}