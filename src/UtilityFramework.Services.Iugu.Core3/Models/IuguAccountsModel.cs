using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguAccountsModel : IuguBaseErrors
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("items")]
        public List<IuguAccountItemModel> Items { get; set; } = new();
    }
}