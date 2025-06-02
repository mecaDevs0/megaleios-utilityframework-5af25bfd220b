using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class FilterSellerViewModel
    {
        [JsonProperty("taxpayer_id")]
        public string TaxpayerId { get; set; }
        [JsonProperty("ein")]
        public string Ein { get; set; }
    }
}