using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class ContainerViewModel
    {

        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)]
        public string Format { get; set; }
    }



}