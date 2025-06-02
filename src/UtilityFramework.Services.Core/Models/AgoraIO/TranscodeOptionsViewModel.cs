using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class TranscodeOptionsViewModel
    {

        [JsonProperty("container")]
        public ContainerViewModel Container { get; set; }

        [JsonProperty("transConfig")]
        public TransConfigViewModel TransConfig { get; set; }

        [JsonProperty("audio")]
        public AudioViewModel Audio { get; set; }
    }



}