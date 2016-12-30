using Flights.Infrastructure.Interfaces;
using Flights.Models;
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
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private readonly IFlightsService _flightsService;
        private bool _isFlightsExist;
        private bool _isLoading;
        private ObservableCollection<FlyInfoShow> _flightsList;
        private DataOfFlights _dataOfFlights;

        public ICommand ShowFlightDetailsCommand { get; set; }

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
            IFlightsService flightsService)
        {
            _jsonConverter = jsonConverter;
            _flightsService = flightsService;
            _fileStore = fileStore;
            _flightsList = new ObservableCollection<FlyInfoShow>();

            ShowFlightDetailsCommand = new MvxCommand(ShowFlyDetails);
        }

        public async Task Init(string param)
        {
            IsFlightsExist = true;
            _dataOfFlights = _jsonConverter.Deserialize<DataOfFlights>(param);
            await ShowFlightsAsync();
            IsFlightsExist = FlightsList.Any() ? true : false;
        }

        private void ShowFlyDetails()
        {
            ShowViewModel<FlightsInfoViewModel>(FlightsList[0]);
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

            IsLoading = false;
        }

        private async Task InitializeDataAsync(string date, List<string> from, List<string> to, bool isReversed = false)
        {
            var flyInfoOneWay = await _flightsService.ConfigurationOfFlightsAsync(date, from, to);
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
    }
}
