using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAdvanceSimulationResponse : IuguBaseErrors
    {
        public IuguAdvanceSimulationResponse()
        {
            Transactions = new List<IuguAdvanceTransactionResponse>();
        }
        [JsonProperty("transactions")]
        public List<IuguAdvanceTransactionResponse> Transactions { get; set; }

        [JsonProperty("total")]
        public IuguTotalAdvanceSimulationResponse Total { get; set; }
    }
}