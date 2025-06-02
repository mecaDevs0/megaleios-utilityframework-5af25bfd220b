using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{
    public class DurationInTrafficViewModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }



}