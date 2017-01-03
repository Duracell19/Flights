using Flights.Infrastructure.Interfaces;
using Flights.Services.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flights.Services.Helpers
{
    public class CitiesService 
    {
        private readonly IHttpService _httpService;
        private readonly IJsonConverter _jsonConverter;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpService">Parameter to work with http service</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        public CitiesService(IHttpService httpService, IJsonConverter jsonConverter)
        {
            _httpService = httpService;
            _jsonConverter = jsonConverter;
        }
        /// <summary>
        /// The method witch get cities by specific country
        /// </summary>
        /// <param name="country">The name of country</param>
        /// <returns>List of cities</returns>
        public async Task<List<string>> GetCitiesAsync(string country)
        {
            var uri = "http://flybaseapi.azurewebsites.net/odata/country('" + country + "')";
            var response = await _httpService.GetRequestAsync(uri);
            if (response != null)
            {
                var airportInfo = _jsonConverter.Deserialize<AirportInfo>(response);
                var cities = new List<string>();
                foreach (var item in airportInfo.value)
                {
                    cities.Add(item.City);
                }
                cities.Sort();
                return cities.Distinct().ToList();
            }
            return null;
        }
    }
}
