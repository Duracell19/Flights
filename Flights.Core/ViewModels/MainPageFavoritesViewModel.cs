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
        /// <summary>
        /// Properties to binding View and ViewModel
        /// </summary>
        public ObservableCollection<Favorite> FavoriteList
        {
            get { return _favoriteList; }
            set
            {
                _favoriteList = value;
                RaisePropertyChanged(() => FavoriteList);
            }
        }
        /// <summary>
        /// Initialization of commands
        /// </summary>
        public ICommand ShowInfoAboutFlights { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SetFlightCommand { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        /// <param name="fileStore">Parameter to work with files</param>
        public MainPageFavoritesViewModel(IJsonConverter jsonConverter, IFileStore fileStore)
        {
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            FavoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);

            ShowInfoAboutFlights = new MvxCommand(() => ShowViewModel<AboutFlightsViewModel>());
            RefreshCommand = new MvxCommand(Refresh);
            SetFlightCommand = new MvxCommand<object>(SetFlight);
        }
        /// <summary>
        /// Implementation of commands
        /// </summary>
        private void Refresh()
        {
            FavoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);
        }

        private void SetFlight(object arg)
        {
            if (arg is Favorite)
            {
                var item = (Favorite)arg;

            }
        }
    }
}
