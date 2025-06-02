using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class PolylineViewModel
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }



}