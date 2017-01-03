using Flights.Infrastructure;
using Flights.Infrastructure.Interfaces;
using Flights.Models;
using Flights.Services.Helpers;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flights.Core.ViewModels
{
    public class FlightsListViewModel : MvxViewModel
    {
        private readonly IHttpService _httpService;
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private bool _isFlightsExist;
        private bool _isLoading;
        private bool _isFlightAlreadyInFavorite;
        private ObservableCollection<Favorite> _favoriteList;
        private ObservableCollection<FlyInfoShow> _flightsList;
        private DataOfFlights _dataOfFlights;

        public ICommand ShowInfoAboutFlights { get; set; }
        public ICommand ShowFlightDetailsCommand { get; set; }
        public ICommand AddToFavoritesCommand { get; set; }
        
        public bool IsFlightsExist
        {
            get { return _isFlightsExist; }
            set
            {
                _isFlightsExist = value;
                RaisePropertyChanged(() => IsFlightsExist);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public bool IsFlightAlreadyInFavorite
        {
            get { return _isFlightAlreadyInFavorite; }
            set
            {
                _isFlightAlreadyInFavorite = value;
                RaisePropertyChanged(() => IsFlightAlreadyInFavorite);
            }
        }

        public ObservableCollection<FlyInfoShow> FlightsList
        {
            get { return _flightsList; }
            set
            {
                _flightsList = value;
                RaisePropertyChanged(() => FlightsList);
            }
        }

        public FlightsListViewModel(
            IJsonConverter jsonConverter,
            IFileStore fileStore,
            IHttpService httpService)
        {
            _httpService = httpService;
            _jsonConverter = jsonConverter;
            _fileStore = fileStore;
            _flightsList = new ObservableCollection<FlyInfoShow>();

            ShowInfoAboutFlights = new MvxCommand(() => ShowViewModel<AboutFlightsViewModel>());
            AddToFavoritesCommand = new MvxCommand(AddToFavorites);
            ShowFlightDetailsCommand = new MvxCommand<object>(ShowFlyDetails);
        }

        public async Task Init(string param)
        {
            IsFlightsExist = true;
            _dataOfFlights = _jsonConverter.Deserialize<DataOfFlights>(param);
            _favoriteList = _fileStore.Load<ObservableCollection<Favorite>>(Defines.FAVORITE_LIST_FILE_NAME);
            _favoriteList = _favoriteList ?? new ObservableCollection<Favorite>();
            await ShowFlightsAsync();
            IsFlightsExist = FlightsList.Any() ? true : false;
        }

        private void AddToFavorites()
        {
            AddFavorite();
            _fileStore.Save(Defines.FAVORITE_LIST_FILE_NAME, _favoriteList);
            IsFlightAlreadyInFavorite = true;
        }

        private void ShowFlyDetails(object arg)
        {
            if (arg is FlyInfoShow)
            {
                var item = (FlyInfoShow)arg;
                ShowViewModel<FlightsInfoViewModel>(arg);
            }
        }

        private void AddFavorite()
        {
            _favoriteList.Add(new Favorite
            {
                CitiesFrom = _dataOfFlights.CitiesFrom,
                CitiesTo = _dataOfFlights.CitiesTo,
                CityFrom = _dataOfFlights.CityFrom,
                CityTo = _dataOfFlights.CityTo,
                CountryFrom = _dataOfFlights.CountryFrom,
                CountryTo = _dataOfFlights.CountryTo,
                IataFrom = _dataOfFlights.IatasFrom,
                IataTo = _dataOfFlights.IatasTo
            });
        }

        private async Task ShowFlightsAsync()
        {
            IsLoading = true;

            await InitializeDataAsync(
                _dataOfFlights.DateOneWay,
                _dataOfFlights.IatasFrom,
                _dataOfFlights.IatasTo);

            if (_dataOfFlights.ReturnWay)
            {
                await InitializeDataAsync(
                _dataOfFlights.DateReturn,
                _dataOfFlights.IatasTo,
                _dataOfFlights.IatasFrom,
                _dataOfFlights.ReturnWay);
            }

            IsFlightAlreadyInFavorite = _favoriteList.Any(IsFlightEqualOfFavorite);
            IsLoading = false;
        }

        private async Task InitializeDataAsync(string date, List<string> from, List<string> to, bool isReversed = false)
        {
            var flightsService = new FlightsService(_httpService, _jsonConverter);
            var flyInfoOneWay = await flightsService.ConfigurationOfFlightsAsync(date, from, to);
            AddToFlightsList(flyInfoOneWay, isReversed);
        }

        private void AddToFlightsList(List<FlyInfo> flyInfoModel, bool isReversedFlight = false)
        {
            foreach (var item in flyInfoModel)
            {
                FlightsList.Add(CreateFlyInfoShowModel(item, isReversedFlight));
            }
        }

        private FlyInfoShow CreateFlyInfoShowModel(FlyInfo infoModel, bool isReversedFlight = false)
        {
            return new FlyInfoShow
            {
                Arrival = infoModel.Arrival,
                Duration = infoModel.Duration,
                ArrivalTerminal = infoModel.ArrivalTerminal,
                From = infoModel.From,
                ThreadCarrierTitle = infoModel.ThreadCarrierTitle,
                ThreadVehicle = infoModel.ThreadVehicle,
                ThreadNumber = infoModel.ThreadNumber,
                Departure = infoModel.Departure,
                To = infoModel.To,
                IsReservedFlight = isReversedFlight
            };
        }

        private bool IsFlightEqualOfFavorite(Favorite model)
        {
            return model.CountryFrom == _dataOfFlights.CountryFrom && model.CityFrom == _dataOfFlights.CityFrom
                   && model.CountryTo == _dataOfFlights.CountryTo && model.CityTo == _dataOfFlights.CityTo;
        }
    }
}
