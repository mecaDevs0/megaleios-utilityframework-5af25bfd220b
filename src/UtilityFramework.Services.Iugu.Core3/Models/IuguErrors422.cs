using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguErrors422
    {
        [JsonProperty("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}