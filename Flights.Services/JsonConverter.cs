using Flights.Infrastructure.Interfaces;
using Newtonsoft.Json;

namespace Flights.Services
{
    public class JsonConverter : IJsonConverter
    {
        /// <summary>
        /// Deserialize data
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="str">Data</param>
        /// <returns>Deserialized data</returns>
        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// Serialize data
        /// </summary>
        /// <param name="obj">Data</param>
        /// <returns>Serialized data</returns>
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
