using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class TransConfigViewModel
    {

        [JsonProperty("transMode", NullValueHandling = NullValueHandling.Ignore)]
        public string TransMode { get; set; }
    }



}