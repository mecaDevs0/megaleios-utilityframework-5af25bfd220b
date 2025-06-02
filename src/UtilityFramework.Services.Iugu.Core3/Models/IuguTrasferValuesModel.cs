using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguTrasferValuesModel
    {
        [JsonProperty("receiver_id")]
        public string Receive { get; set; }
        [JsonProperty("amount_cents")]
        public int AmoutCents { get; set; }
        [JsonProperty("api_token")]
        public string ApiToken { get; set; }
    }
}