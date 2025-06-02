using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguDocumentStatusModel
    {
        /// <summary>
        /// CAMPO
        /// </summary>
        [JsonProperty("kind", NullValueHandling = NullValueHandling.Ignore)]
        public string Kind { get; set; }
        /// <summary>
        /// STATUS
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public FileDocumentStatus? Status { get; set; }
        /// <summary>
        /// Data de envio
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Data da última atualização
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }
    }
}