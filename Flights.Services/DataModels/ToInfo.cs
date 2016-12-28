using Newtonsoft.Json;

namespace Flights.Services.DataModels
{
    public class ToInfo
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
