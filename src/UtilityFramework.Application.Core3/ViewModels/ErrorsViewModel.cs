using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels
{
    public class Errors422ViewModel
    {
        [JsonProperty("errors")]
        public Dictionary<string, List<string>> Errors { get; set; }
    }

    public class ErrorsViewModel
    {
        [JsonProperty("errors")]
        public string Errors { get; set; }

    }
}