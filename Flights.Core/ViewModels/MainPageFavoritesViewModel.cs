using Flights.Infrastructure;
using Flights.Infrastructure.Interfaces;
using Flights.Models;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flights.Core.ViewModels
{
    public class MainPageFavoritesViewModel : MvxViewModel
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private ObservableCollection<Favorite> _favoriteList;

        public ObservableCollection<Favorite> FavoriteList
        {
            get { return _favoriteList; }
            set
            {
                _favoriteList = value;
                RaisePropertyChanged(() => FavoriteList);
            }
        }

        public ICommand RefreshCommand { get; set; }

        public MainPageFavoritesViewModel(IJsonConverter jsonConverter, IFileStore fileStore)
        {
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            FavoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);

            RefreshCommand = new MvxCommand(Refresh);
        }

        private void Refresh()
        {
            FavoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);
        }
    }
}
