using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core
{
    public class ZoopSettings
    {
        [JsonProperty("dev")]
        public ZoopConfiguration Dev { get; set; }
        [JsonProperty("prod")]
        public ZoopConfiguration Prod { get; set; }

        [JsonProperty("sandBox")]
        public bool SandBox { get; set; }
    }

    public class ZoopConfiguration
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("marketPlaceId")]
        public string MarketPlaceId { get; set; }
        [JsonProperty("masterSellerId")]
        public string MasterSellerId { get; set; }
    }


}