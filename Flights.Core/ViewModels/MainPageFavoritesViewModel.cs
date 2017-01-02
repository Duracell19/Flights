using Flights.Infrastructure;
using Flights.Infrastructure.Interfaces;
using Flights.Models;
using MvvmCross.Core.ViewModels;
using System.Collections.ObjectModel;

namespace Flights.Core.ViewModels
{
    public class MainPageFavoritesViewModel : MvxViewModel
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        public ObservableCollection<Favorite> FavoriteList { get; set; }

        public MainPageFavoritesViewModel(IJsonConverter jsonConverter, IFileStore fileStore)
        {
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            FavoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);
        }
    }
}
