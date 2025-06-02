using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class StepViewModel
    {
        [JsonProperty("distance")]
        public DistanceViewModel Distance { get; set; }

        [JsonProperty("duration")]
        public DurationViewModel Duration { get; set; }

        [JsonProperty("end_location")]
        public EndLocationViewModel EndLocation { get; set; }

        [JsonProperty("html_instructions")]
        public string HtmlInstructions { get; set; }

        [JsonProperty("polyline")]
        public PolylineViewModel Polyline { get; set; }

        [JsonProperty("start_location")]
        public StartLocationViewModel StartLocation { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }

        [JsonProperty("maneuver")]
        public string Maneuver { get; set; }
    }



}