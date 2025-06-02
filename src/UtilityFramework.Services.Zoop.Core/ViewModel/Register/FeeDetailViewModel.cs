using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class FeeDetailViewModel
    {

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("prepaid")]
        public bool Prepaid { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("is_gateway_fee")]
        public bool IsGatewayFee { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}