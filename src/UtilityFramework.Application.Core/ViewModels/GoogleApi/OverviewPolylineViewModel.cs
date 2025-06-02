using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class OverviewPolylineViewModel
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }



}