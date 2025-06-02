using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class LegViewModel
    {
        [JsonProperty("distance")]
        public DistanceViewModel Distance { get; set; }

        [JsonProperty("duration")]
        public DurationViewModel Duration { get; set; }

        [JsonProperty("duration_in_traffic")]
        public DurationInTrafficViewModel DurationInTraffic { get; set; }

        [JsonProperty("end_address")]
        public string EndAddress { get; set; }

        [JsonProperty("end_location")]
        public EndLocationViewModel EndLocation { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("start_location")]
        public StartLocationViewModel StartLocation { get; set; }

        [JsonProperty("steps")]
        public List<StepViewModel> Steps { get; set; } = new List<StepViewModel>();

        [JsonProperty("traffic_speed_entry")]
        public List<object> TrafficSpeedEntry { get; set; }

        [JsonProperty("via_waypoint")]
        public List<ViaWaypointViewModel> ViaWaypoint { get; set; } = new List<ViaWaypointViewModel>();
    }



}