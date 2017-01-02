using Android.App;
using Android.OS;
using Acr.UserDialogs;
using MvvmCross.Droid.Views;
using Android.Views;
using Android.Widget;

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

            var listView = FindViewById<ListView>(Resource.Id.favoriteList);
            listView.ItemLongClick += listView_ItemLongClick;
        }

        private void listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var menu = new PopupMenu(this, (View)sender);
            menu.Inflate(Resource.Menu.flate_menu);
            menu.MenuItemClick += (s, a) =>
            {
                switch (a.Item.ItemId)
                {
                    case Resource.Id.pm_delete:
                        var index = e.Position;

                        break;
                }
            };
            menu.Show();
        }
    }
}