﻿using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguPricesModel
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("value_cents")]
        public string ValueCents { get; set; }
    }
}