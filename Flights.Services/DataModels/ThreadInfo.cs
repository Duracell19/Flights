using Newtonsoft.Json;

namespace Flights.Services.DataModels
{
    public class ThreadInfo
    {
        [JsonProperty(PropertyName = "carrier")]
        public CarrierInfo Carrier { get; set; }
        [JsonProperty(PropertyName = "vehicle")]
        public string Vehicle { get; set; }
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }
    }
}
