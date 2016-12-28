using Flights.Infrastructure.Interfaces;
using Flights.Models;
using MvvmCross.Core.ViewModels;

namespace Flights.Core.ViewModels
{
    public class FlightsListViewModel : MvxViewModel
    {
        private readonly IJsonConverter _jsonConverter;
        private readonly IFileStore _fileStore;
        private readonly IFlightsService _flightsService;
        private DataOfFlights _dataOfFlights;
                
        public FlightsListViewModel(
            IJsonConverter jsonConverter,
            IFileStore fileStore,
            IFlightsService flightsService)
        {
            _jsonConverter = jsonConverter;
            _flightsService = flightsService;
            _fileStore = fileStore;
        }

        public void Init(string param)
        {
            _dataOfFlights = _jsonConverter.Deserialize<DataOfFlights>(param);
        }
    }
}
