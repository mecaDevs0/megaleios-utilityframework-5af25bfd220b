using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class LayoutConfigViewModel
    {

        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }

        [JsonProperty("x_axis", NullValueHandling = NullValueHandling.Ignore)]
        public double? XAxis { get; set; }

        [JsonProperty("y_axis", NullValueHandling = NullValueHandling.Ignore)]
        public double? YAxis { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public double? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public double? Height { get; set; }

        [JsonProperty("alpha", NullValueHandling = NullValueHandling.Ignore)]
        public double? Alpha { get; set; }

        [JsonProperty("render_mode", NullValueHandling = NullValueHandling.Ignore)]
        public int? RenderMode { get; set; }
    }
}