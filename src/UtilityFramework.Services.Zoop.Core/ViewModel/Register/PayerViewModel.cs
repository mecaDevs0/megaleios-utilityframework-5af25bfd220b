using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class PayerViewModel
    {
        /// <summary>
        /// REFERENCIA DO PAGADOR  | TOKEN | CUSTOMER/BUYER |  
        /// </summary>
        /// <value></value>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// VALOR EM CENTAVOS 
        /// </summary>
        /// <value></value>

        [JsonProperty("amount")]
        public long Amount { get; set; }
    }

}