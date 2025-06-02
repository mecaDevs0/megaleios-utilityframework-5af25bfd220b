using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Response
{
    public class IuguMarketPlaceCredentials
    {

        [JsonProperty("live_token")]
        public string LiveToken { get; set; }

        [JsonProperty("test_token")]
        public string TestToken { get; set; }

        [JsonProperty("user_token")]
        public string UserToken { get; set; }
    }
}