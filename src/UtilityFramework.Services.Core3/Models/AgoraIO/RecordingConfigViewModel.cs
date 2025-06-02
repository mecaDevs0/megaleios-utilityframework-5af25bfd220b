using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class RecordingConfigViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxIdleTime { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? StreamTypes { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ChannelType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? VideoStreamType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TranscodingConfigViewModel TranscodingConfig { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SubscribeAudioUids { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SubscribeVideoUids { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? SubscribeUidGroup { get; set; }
    }
}