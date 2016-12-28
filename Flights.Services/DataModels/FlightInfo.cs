using Newtonsoft.Json;
using System.Collections.Generic;

namespace Flights.Services.DataModels
{
    public class FlightInfo
    {
        [JsonProperty(PropertyName = "threads")]
        public List<ThreadsInfo> Threads { get; set; }
    }
}
