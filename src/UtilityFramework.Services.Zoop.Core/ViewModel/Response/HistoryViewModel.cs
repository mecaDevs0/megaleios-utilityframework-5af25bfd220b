using System;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class HistoryViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("response_code")]
        public object ResponseCode { get; set; }

        [JsonProperty("response_message")]
        public object ResponseMessage { get; set; }

        [JsonProperty("authorization_code")]
        public object AuthorizationCode { get; set; }

        [JsonProperty("authorizer_id")]
        public object AuthorizerId { get; set; }

        [JsonProperty("authorization_nsu")]
        public object AuthorizationNsu { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

    }

}