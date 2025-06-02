using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterTokenBankAccountViewModel
    {

        /// <summary>
        /// NOME DO RESPONSAVEL
        /// </summary>
        /// <value></value>
        [JsonProperty("holder_name")]
        public string HolderName { get; set; }
        /// <summary>
        /// CODIGO DO BANCO 
        /// </summary>
        /// <value></value>
        [JsonProperty("bank_code")]
        public string BankCode { get; set; }
        /// <summary>
        /// NÚMERO DA AGENCIA SEM DIGITO
        /// </summary>
        /// <value></value>
        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }
        /// <summary>
        /// NÚMERO DA CONTA COM DIGITO
        /// </summary>
        /// <value></value>

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }
        /// <summary>
        /// CPF NO CASO DE PF
        /// </summary>
        /// <value></value>
        [JsonProperty("taxpayer_id")]
        public string TaxpayerId { get; set; }
        /// <summary>
        /// CNPJ NO  CASO PJ
        /// </summary>
        /// <value></value>
        [JsonProperty("ein")]
        public string Ein { get; set; }
        /// <summary>
        /// TIPO DE CONTA CORRENTE / POUPANÇA
        /// </summary>
        /// <value></value>
        [JsonProperty("type")]
        public TypeBankAccount Type { get; set; }
    }

}