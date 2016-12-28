using Newtonsoft.Json;

namespace Flights.Services.DataModels
{
    public class FromInfo
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
