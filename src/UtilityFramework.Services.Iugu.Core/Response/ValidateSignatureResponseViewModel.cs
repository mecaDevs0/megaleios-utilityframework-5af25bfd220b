using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Models;


namespace UtilityFramework.Services.Iugu.Core.Response
{
    public class ValidadeSignatureResponseViewModel : IuguBaseErrors
    {

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("request_body")]
        public string RequestBody { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
