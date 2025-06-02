﻿using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAccountFilterModel : IuguBaseErrors
    {
        public IuguAccountFilterModel()
        {
            Limit = "100";
            Start = "1";
            Query = "";

        }

        [JsonProperty("limit")]
        public string Limit { get; set; }
        [JsonProperty("start")]
        public string Start { get; set; }
        [JsonProperty("query")]
        public string Query { get; set; }
    }
}