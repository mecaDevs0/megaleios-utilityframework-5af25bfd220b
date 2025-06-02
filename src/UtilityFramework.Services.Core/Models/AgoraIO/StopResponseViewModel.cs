using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class StopResponseViewModel : BaseResponseViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Sid { get; set; }

    }
}
