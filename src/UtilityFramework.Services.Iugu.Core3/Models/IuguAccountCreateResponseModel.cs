using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguAccountCreateResponseModel : IuguBaseErrors
    {
        [JsonProperty("account_id", NullValueHandling = NullValueHandling.Ignore)]
        public string AccountId { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("live_api_token", NullValueHandling = NullValueHandling.Ignore)]
        public string LiveApiToken { get; set; }
        [JsonProperty("test_api_token", NullValueHandling = NullValueHandling.Ignore)]
        public string TestApiToken { get; set; }
        [JsonProperty("user_token", NullValueHandling = NullValueHandling.Ignore)]
        public string UserToken { get; set; }
    }
}