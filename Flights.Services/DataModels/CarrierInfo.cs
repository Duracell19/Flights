using Newtonsoft.Json;

namespace Flights.Services.DataModels
{
    public class CarrierInfo
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
