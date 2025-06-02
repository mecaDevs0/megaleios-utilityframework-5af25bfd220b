using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class ListIuguAdvanceResponse
    {
        public ListIuguAdvanceResponse()
        {
            Items = new List<ItemAdvance>();
        }

        [JsonProperty("totalItems")]
        public long TotalItems { get; set; }

        [JsonProperty("items")]
        public List<ItemAdvance> Items { get; set; }
    }
}