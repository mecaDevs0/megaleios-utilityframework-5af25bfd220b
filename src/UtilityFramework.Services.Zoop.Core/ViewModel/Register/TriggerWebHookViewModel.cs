using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{

    public class TriggerWebHookViewModel<T>
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("payload")]
        public PayloadViewModel<T> Payload { get; set; }

        [JsonProperty("source")]
        public SourceViewModel Source { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}