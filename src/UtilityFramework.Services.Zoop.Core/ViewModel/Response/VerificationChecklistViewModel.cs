using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class VerificationChecklistViewModel
    {
        [JsonProperty("postal_code_check")]
        public string PostalCodeCheck { get; set; }

        [JsonProperty("security_code_check")]
        public string SecurityCodeCheck { get; set; }

        [JsonProperty("address_line1_check")]
        public string AddressLine1Check { get; set; }
    }
}