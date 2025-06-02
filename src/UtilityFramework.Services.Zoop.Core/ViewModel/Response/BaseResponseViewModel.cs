using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class BaseResponseViewModel<T> : BaseErrorViewModel
    {
        public BaseResponseViewModel()
        {
            Items = new List<T>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("used")]
        public bool Used { get; set; }
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("card")]
        public CardViewModel Card { get; set; }

        [JsonProperty("bank_account")]
        public BankAccountViewModel BankAccount { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("offset")]
        public int? Offset { get; set; }

        [JsonProperty("has_more")]
        public bool? HasMore { get; set; }

        [JsonProperty("query_count")]
        public int? QueryCount { get; set; }

        [JsonProperty("total")]
        public int? Total { get; set; }
    }

}