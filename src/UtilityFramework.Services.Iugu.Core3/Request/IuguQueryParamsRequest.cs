using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Request
{
    public class IuguQueryParamsRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }
        [JsonProperty("start")]
        public int? Start { get; set; }
    }
}