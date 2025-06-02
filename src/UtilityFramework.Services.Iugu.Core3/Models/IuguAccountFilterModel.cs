using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguAccountFilterModel : IuguBaseErrors
    {
        [JsonProperty("limit")]
        public int? Limit { get; set; }
        [JsonProperty("start")]
        public int? Start { get; set; }
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}