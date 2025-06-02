using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class ResponseWebHookViewModel : BaseErrorViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("events")]
        public List<string> Events { get; set; }

        [JsonProperty("last_error")]
        public object LastError { get; set; }

        [JsonProperty("retries")]
        public long? Retries { get; set; }

        [JsonProperty("events_sent")]
        public long? EventsSent { get; set; }

        [JsonProperty("batches_sent")]
        public long? BatchesSent { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("last_sent_at")]
        public object LastSentAt { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}