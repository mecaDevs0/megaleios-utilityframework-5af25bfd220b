using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Response
{

    public class WebHookViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("web_hook_id")]
        public string WebHookId { get; set; }

        [JsonProperty("data")]
        public DataWebHookViewModel Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("loggable_id")]
        public string LoggableId { get; set; }

        [JsonProperty("loggable_type")]
        public string LoggableType { get; set; }
    }

}