using Newtonsoft.Json;

namespace Flights.Services.DataModels
{
    public class ThreadsInfo
    {
        [JsonProperty(PropertyName = "arrival")]
        public string Arrival { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }
        [JsonProperty(PropertyName = "arrival_terminal")]
        public string ArrivalTerminal { get; set; }
        [JsonProperty(PropertyName = "from")]
        public FromInfo From { get; set; }
        [JsonProperty(PropertyName = "thread")]
        public ThreadInfo Thread { get; set; }
        [JsonProperty(PropertyName = "departure")]
        public string Departure { get; set; }
        [JsonProperty(PropertyName = "to")]
        public ToInfo To { get; set; }
    }
}
