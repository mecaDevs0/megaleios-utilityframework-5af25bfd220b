using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguTriggerModel
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("data")]
        public DataTriggerModel Data { get; set; }

    }
}