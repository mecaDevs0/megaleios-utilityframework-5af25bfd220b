using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class IuguMarketPlaceConfigResponse : IuguBaseErrors
    {

        [JsonProperty("referrer_id")]
        public string ReferrerId { get; set; }

        [JsonProperty("accounts")]
        public Dictionary<string, IuguMarketPlaceCredentials> Accounts { get; set; } = new();
    }
}