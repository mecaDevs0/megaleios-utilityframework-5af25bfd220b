using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RequestTransferViewModel
    {

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Date dd/mm/yyyy
        /// </summary>
        /// <value></value>
        [JsonProperty("transfer_date")]
        public string TransferDate { get; set; }
    }
}