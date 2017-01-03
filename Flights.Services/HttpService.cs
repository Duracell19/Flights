using Flights.Infrastructure.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flights.Services
{
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Get responce from service
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Responce</returns>
        public async Task<string> GetRequestAsync(string url)
        {
            try
            {
                var client = new HttpClient();
                var uri = new Uri(url);
                var response = await client.GetStringAsync(uri);
                client.Dispose();
                return response;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
