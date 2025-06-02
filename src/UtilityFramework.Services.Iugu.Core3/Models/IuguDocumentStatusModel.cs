using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Enums;

namespace UtilityFramework.Services.Iugu.Core3.Models
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