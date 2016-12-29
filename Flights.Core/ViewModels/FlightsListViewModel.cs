using Flights.Infrastructure.Interfaces;
using Flights.Models;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flights.Core.ViewModels
{
    public class FlightsListViewModel : MvxViewModel
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private readonly IFlightsService _flightsService;
        private FlyInfoShowModel _selectedItem;
        private ObservableCollection<FlyInfoShowModel> _flightsList;
        private DataOfFlights _dataOfFlights;

        public ICommand ShowFlightDetailsCommand { get; set; }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged(() => Path);
            }
        }
        public FlyInfoShowModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ObservableCollection<FlyInfoShowModel> FlightsList
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
            _selectedItem = new FlyInfoShowModel();
            _flightsList = new ObservableCollection<FlyInfoShowModel>();

            ShowFlightDetailsCommand = new MvxCommand(ShowFlyDetails);
        }

        public async Task Init(string param)
        {
            _dataOfFlights = _jsonConverter.Deserialize<DataOfFlights>(param);
            await ShowFlightsAsync();
        }

        private void ShowFlyDetails()
        {

        }

        private async Task ShowFlightsAsync()
        {
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

        private FlyInfoShowModel CreateFlyInfoShowModel(FlyInfo infoModel, bool isReversedFlight = false)
        {
            return new FlyInfoShowModel
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
