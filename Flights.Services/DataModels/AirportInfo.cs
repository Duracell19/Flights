using Newtonsoft.Json;
using System.Collections.Generic;

namespace Flights.Services.DataModels
{
    public class AirportInfo
    {
        [JsonProperty(PropertyName = "value")]
        public List<Value> value { get; set; }
    }
}
