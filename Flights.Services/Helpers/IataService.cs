using Flights.Infrastructure.Interfaces;
using Flights.Services.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.Services.Helpers
{
    public class IataService
    {
        readonly IHttpService _httpService;
        readonly IJsonConverter _jsonConverter;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpService">Parameter to work with http service</param>
        /// <param name="jsonConverter">Parameter to work with json converter</param>
        public IataService(IHttpService httpService, IJsonConverter jsonConverter)
        {
            _httpService = httpService;
            _jsonConverter = jsonConverter;
        }
        /// <summary>
        /// The method witch get iatas by specific country
        /// </summary>
        /// <param name="city">The name of city</param>
        /// <returns>List of iatas</returns>
        public async Task<List<string>> GetIataAsync(string city)
        {
            var uri = "http://flybaseapi.azurewebsites.net/odata/code_iata('" + city + "')";
            var response = await _httpService.GetRequestAsync(uri);
            if (response != null)
            {
                var airportInfo = _jsonConverter.Deserialize<AirportInfo>(response);
                var iata = new List<string>();
                foreach (var item in airportInfo.value)
                {
                    iata.Add(item.Iata);
                }
                return iata;
            }
            return null;
        }
    }

}
