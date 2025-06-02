using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterWebHookViewModel
    {

        /// <summary>
        /// URL DE CALLBACK
        /// </summary>
        /// <value></value>
        [JsonProperty("url")]
        public string Url { get; set; }
        /// <summary>
        /// TYPE METHOD HTTP
        /// </summary>
        /// <value></value>

        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// DESCRIÇÃO DO WEBHOOK
        /// </summary>
        /// <value></value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// list de eventos
        /// </summary>
        /// <value></value>
        [JsonProperty("event")]
        public List<WebHookEvents> Event { get; set; }
    }

}