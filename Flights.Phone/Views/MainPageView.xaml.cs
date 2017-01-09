using Flights.Infrastructure;
using Flights.Models;
using MvvmCross.WindowsCommon.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Flights.Phone.Views
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        private ListView _listView;
        private ObservableCollection<Favorite> _favorite;

        public ObservableCollection<Favorite> FavoriteList
        {
            get { return _favorite; }
            set { _favorite = value; }
        }

        public MainPageView()
        {
            this.InitializeComponent();
        }
        /// <summary>
        /// This is command show flyoutBase menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMenuOfFlight(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            var senderElement = sender as FrameworkElement;
            var flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }
        /// <summary>
        /// This is command delete favorite flight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteCommand(object sender, RoutedEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement).DataContext;
            _listView = (ListView)FindChildByName(this, "favoriteList");
            FavoriteList = (ObservableCollection<Favorite>)_listView.ItemsSource;
            FavoriteList.Remove((Favorite)item);
            WriteToFileAsync(FavoriteList);
        }
        /// <summary>
        /// This is method write to file favorite flights
        /// </summary>
        /// <param name="obj">flights list</param>
        /// <returns></returns>
        private async Task WriteToFileAsync(object obj)
        {
            var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(Defines.FAVORITE_LIST_FILE_NAME, CreationCollisionOption.ReplaceExisting);
            var sw = new StreamWriter(stream);
            await sw.WriteAsync(JsonConvert.SerializeObject(obj));
            sw.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            stream.Dispose();
        }
        /// <summary>
        /// This is method find control by name
        /// </summary>
        /// <param name="from"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static DependencyObject FindChildByName(DependencyObject from, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(from);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(from, i);
                if (child is FrameworkElement && ((FrameworkElement)child).Name == name)
                {
                    return child;
                }
                var result = FindChildByName(child, name);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
