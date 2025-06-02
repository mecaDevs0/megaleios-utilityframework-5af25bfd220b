using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class LocationViewModel
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }



}