using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models
{
    public class SmsCredentialsViewModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}