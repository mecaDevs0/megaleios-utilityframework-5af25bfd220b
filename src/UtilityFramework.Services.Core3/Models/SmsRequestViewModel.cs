using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models
{
    public class SmsRequestViewModel
    {
            [JsonProperty(PropertyName = "from")]
            public string From { get; set; }
            [JsonProperty(PropertyName = "to")]
            public string To { get; set; }
            [JsonProperty(PropertyName = "text")]
            public string Text { get; set; }
    }
}