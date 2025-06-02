using System;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class IuguBankVerificationResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("agency")]
        public string Agency { get; set; }
        [JsonProperty("operation")]
        public object Operation { get; set; }
        [JsonProperty("feedback")]
        public string Feedback { get; set; }
        [JsonProperty("bank")]
        public string Bank { get; set; }
    }
}