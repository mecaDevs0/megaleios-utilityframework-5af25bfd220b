using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class PaymentAuthorizationViewModel
    {

        [JsonProperty("authorizer_id")]
        public string AuthorizerId { get; set; }

        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("authorization_nsu")]
        public string AuthorizationNsu { get; set; }
    }
}