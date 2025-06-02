using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity.Lists
{
    public class PlansModel
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("items")]
        public List<PlanModel> Items { get; set; }
    }
}
