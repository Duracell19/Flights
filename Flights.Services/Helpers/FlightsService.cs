using Flights.Infrastructure.Interfaces;
using Flights.Models;
using Flights.Services.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Services.Helpers
{
    public class FlightsService
    {
        readonly IHttpService _httpService;
        readonly IJsonConverter _jsonConverter;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpService">Parameter to work with http service</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        public FlightsService(IHttpService httpService, IJsonConverter jsonConverter)
        {
            _httpService = httpService;
            _jsonConverter = jsonConverter;
        }
        /// <summary>
        /// The method gets data about flights from service
        /// </summary>
        /// <param name="date">Date of flight</param>
        /// <param name="from">Location from</param>
        /// <param name="to">Location to</param>
        /// <returns></returns>
        public async Task<List<FlyInfo>> GetFlightAsync(string date, string from, string to)
        {
            var uri = "https://api.rasp.yandex.net/v1.0/search/?apikey=e07ef310-dbe4-49cf-985f-1d5738c1ebc7&format=json&transport_types=plane&system=iata&from=" + from + "&to=" + to + "&lang=en&page=1&date=" + date;
            var response = await _httpService.GetRequestAsync(uri);
            if (response == null)
            {
                return null;
            }
            var flightInfo = _jsonConverter.Deserialize<FlightInfo>(response);
            var flyInfo = new List<FlyInfo>();
            flyInfo.AddRange(flightInfo.Threads.Select(CreateFlyInfo));
            return flyInfo;
        }
        /// <summary>
        /// Create a model which contains data about flight
        /// </summary>
        /// <param name="model">Data from service</param>
        /// <returns>Data about flight</returns>
        private FlyInfo CreateFlyInfo(ThreadsInfo model)
        {
            return new FlyInfo
            {
                Arrival = model.Arrival,
                Duration = model.Duration,
                ArrivalTerminal = model.ArrivalTerminal,
                From = model.From.Title,
                ThreadCarrierTitle = model.Thread.Carrier.Title,
                ThreadVehicle = model.Thread.Vehicle,
                ThreadNumber = model.Thread.Number,
                Departure = model.Departure,
                To = model.To.Title
            };
        }
        /// <summary>
        /// Configuration how get flights
        /// </summary>
        /// <param name="date">Date of flight</param>
        /// <param name="iatasFrom">Iatas from</param>
        /// <param name="iatasTo">Iatas to</param>
        /// <returns>List of flight's data</returns>
        public async Task<List<FlyInfo>> ConfigurationOfFlightsAsync(string date, List<string> iatasFrom, List<string> iatasTo)
        {
            var flyInfo = new List<FlyInfo>();
            foreach (var iataFrom in iatasFrom)
            {
                foreach (var iataTo in iatasTo)
                {
                    var result = await GetFlightAsync(date, iataFrom, iataTo);
                    if (result == null)
                    {
                        continue;
                    }
                    flyInfo.AddRange(result);
                }
            }
            return flyInfo;
        }
    }
}
