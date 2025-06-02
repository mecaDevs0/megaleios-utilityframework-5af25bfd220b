using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class IuguMarketPlaceResponse : IuguBaseErrors
    {

        [JsonProperty("items")]
        public List<IuguMarketPlaceItem> Items { get; set; } = new();

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
    }
}