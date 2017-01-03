using Android.App;
using Android.OS;
using Acr.UserDialogs;
using MvvmCross.Droid.Views;
using Android.Views;
using Android.Widget;
using System.Collections.ObjectModel;
using Flights.Models;
using MvvmCross.Platform;
using MvvmCross.Plugins.File;
using Flights.Infrastructure;
using Newtonsoft.Json;

namespace Flights.Droid.Views
{
    [Activity(Label = "Favorites")]
    public class MainPageFavoritesView : MvxActivity
    {
        public ObservableCollection<Favorite> FavoriteList { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MainPageFavoritesView);

            UserDialogs.Init(this);

            var listView = FindViewById<ListView>(Resource.Id.favoriteList);
            listView.ItemLongClick += listView_ItemLongClick;
        }
        /// <summary>
        /// Command exists to show flybase menu and make action with items in this menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var menu = new PopupMenu(this, (View)sender);
            menu.Inflate(Resource.Menu.flate_menu);
            menu.MenuItemClick += (s, a) =>
            {
                switch (a.Item.ItemId)
                {
                    // Delete command
                    case Resource.Id.pm_delete:
                        var index = e.Position;
                        var _fileStore = Mvx.Resolve<IMvxFileStore>();
                        string txt;
                        if (_fileStore.TryReadTextFile(Defines.FAVORITE_LIST_FILE_NAME, out txt))
                        {
                            var items = JsonConvert.DeserializeObject<ObservableCollection<Favorite>>(txt);
                            FavoriteList = items;
                        }
                        var listView = FindViewById<ListView>(Resource.Id.favoriteList);
                        FavoriteList.RemoveAt(index); 
                        _fileStore.WriteFile(Defines.FAVORITE_LIST_FILE_NAME, JsonConvert.SerializeObject(FavoriteList));
                        break;
                }
            };
            menu.Show();
        }
    }
}