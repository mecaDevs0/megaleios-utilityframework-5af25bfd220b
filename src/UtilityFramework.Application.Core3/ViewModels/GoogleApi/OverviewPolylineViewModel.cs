using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{
    public class OverviewPolylineViewModel
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }



}