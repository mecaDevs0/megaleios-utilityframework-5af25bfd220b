using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{
    public class RouteViewModel
    {
        [JsonProperty("bounds")]
        public BoundsViewModel Bounds { get; set; }

        [JsonProperty("copyrights")]
        public string Copyrights { get; set; }

        [JsonProperty("legs")]
        public List<LegViewModel> Legs { get; set; }

        [JsonProperty("overview_polyline")]
        public OverviewPolylineViewModel OverviewPolyline { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("warnings")]
        public List<object> Warnings { get; set; }

        [JsonProperty("waypoint_order")]
        public List<object> WaypointOrder { get; set; }
    }



}