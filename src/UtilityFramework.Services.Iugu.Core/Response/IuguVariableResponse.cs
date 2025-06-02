using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Response
{
    public class IuguVariableResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("variable")]
        public string Variable { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}