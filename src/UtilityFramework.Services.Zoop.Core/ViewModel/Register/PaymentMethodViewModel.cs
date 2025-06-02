using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class PaymentMethodViewModel
    {

        /// <summary>
        /// INSTRUÇÕES NO BOLETO 
        /// </summary>
        /// <value></value>
        [JsonProperty("top_instructions")]
        public List<string> TopInstructions { get; set; }
        /// <summary>
        /// DATA DE VENCIMENTO DO BOLETO DD/MM/YYYY 
        /// </summary>
        /// <value></value>

        [JsonProperty("expiration_date")]
        public string ExpirationDate { get; set; }
    }

}