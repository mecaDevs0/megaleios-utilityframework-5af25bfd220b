using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class ErrorViewModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("category")]

        public CategoryError CategoryError { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("messagecustom")]
        public string MessageCustom { get; set; }
    }
}