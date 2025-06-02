using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class MegaClientIuguViewModel
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("AccountKey")]
        public string AccountKey { get; set; }

        [JsonProperty("LiveKey")]
        public string LiveKey { get; set; }

        [JsonProperty("TestKey")]
        public string TestKey { get; set; }

        [JsonProperty("UserApiKey")]
        public string UserApiKey { get; set; }
        [JsonProperty("Tax")]
        public double Tax { get; set; }
        [JsonProperty("EnableAdvance")]
        public bool EnableAdvance { get; set; }
    }
}