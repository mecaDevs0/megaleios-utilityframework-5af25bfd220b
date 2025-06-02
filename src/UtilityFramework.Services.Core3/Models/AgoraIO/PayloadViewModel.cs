using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class PayloadViewModel
    {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
