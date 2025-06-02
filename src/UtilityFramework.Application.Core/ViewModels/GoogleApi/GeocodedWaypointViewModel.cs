using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class GeocodedWaypointViewModel
    {
        [JsonProperty("geocoder_status")]
        public string GeocoderStatus { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }



}