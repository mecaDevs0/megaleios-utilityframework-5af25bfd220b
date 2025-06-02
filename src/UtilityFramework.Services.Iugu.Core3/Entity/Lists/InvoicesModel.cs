using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Entity;

namespace UtilityFramework.Services.Iugu.Core3.Entity.Lists
{
    public class InvoicesModel
    {
        // TODO: Adicionar descrições
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("facets")]
        public Facets Facets { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("items")]
        public List<InvoiceModel> Items { get; set; }
    }
}
