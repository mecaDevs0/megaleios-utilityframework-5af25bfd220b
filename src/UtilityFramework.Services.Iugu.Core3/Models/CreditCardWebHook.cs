using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class CreditCardWebHook
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("holder")]
        public string Holder { get; set; }

        [JsonProperty("masked_number")]
        public string MaskedNumber { get; set; }

        [JsonProperty("month")]
        public int? Month { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }
    }
}