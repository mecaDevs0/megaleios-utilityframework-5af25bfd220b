using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguErrorsArray
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
    }
}