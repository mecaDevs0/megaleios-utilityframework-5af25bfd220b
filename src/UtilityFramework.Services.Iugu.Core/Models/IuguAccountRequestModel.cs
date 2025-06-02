using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Request;

namespace UtilityFramework.Services.Iugu.Core.Models
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
        public List<SplitModel> splits { get; set; } = new List<SplitModel>();
    }
}