using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class AudioViewModel
    {

        [JsonProperty("sampleRate", NullValueHandling = NullValueHandling.Ignore)]
        public string SampleRate { get; set; }

        [JsonProperty("bitrate", NullValueHandling = NullValueHandling.Ignore)]
        public string Bitrate { get; set; }

        [JsonProperty("channels", NullValueHandling = NullValueHandling.Ignore)]
        public string Channels { get; set; }
    }



}