using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class TranscodingConfigViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Height { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Width { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Bitrate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Fps { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MixedVideoLayout { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundColor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackgroundImage { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<LayoutConfigViewModel> LayoutConfig { get; set; }
    }
}