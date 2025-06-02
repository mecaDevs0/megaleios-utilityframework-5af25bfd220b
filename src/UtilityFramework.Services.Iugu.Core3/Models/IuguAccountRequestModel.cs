using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Request;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguAccountRequestModel : IuguBaseErrors
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("commission_percent ")]
        public int? CommissionPercent { get; set; }
        [JsonProperty("api_token ")]
        public string ApiToken { get; set; }
        [JsonProperty("splits")]
        public List<IuguSplitModel> Splits { get; set; } = [];
    }
}