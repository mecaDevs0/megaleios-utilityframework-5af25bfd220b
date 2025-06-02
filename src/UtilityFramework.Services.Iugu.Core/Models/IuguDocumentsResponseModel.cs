using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguDocumentsResponseModel : IuguBaseErrors
    {
        /// <summary>
        /// Documentos
        /// </summary>
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<IuguDocumentStatusModel> Items { get; set; }
    }
}