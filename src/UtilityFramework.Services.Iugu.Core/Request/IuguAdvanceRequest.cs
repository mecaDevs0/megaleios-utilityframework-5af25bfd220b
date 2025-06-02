using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Request
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