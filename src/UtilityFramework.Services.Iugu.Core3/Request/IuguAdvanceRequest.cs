using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Request
{
    public class IuguAdvanceRequest : IuguBaseErrors
    {
        public IuguAdvanceRequest()
        {
            Transactions = new List<string>();
        }
        [JsonProperty("transactions")]
        public List<string> Transactions { get; set; }
    }
}