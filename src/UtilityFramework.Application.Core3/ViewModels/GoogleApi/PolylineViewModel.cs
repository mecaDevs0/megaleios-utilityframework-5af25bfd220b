using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{
    public class PolylineViewModel
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }



}