using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class BaseTransactionViewModel
    {
        public BaseTransactionViewModel()
        {
            Currency = CurrencyType.BRL;
            SplitRules = new List<SplitRulesViewModel>();
        }
        /// <summary>
        /// TIPO DE PAGAMENTO
        /// </summary>
        /// <value></value>
        [JsonProperty("payment_method")]
        public PaymentMethodViewModel PaymentMethod { get; set; }
        /// <summary>
        /// VALOR DA TRANSAÇÃO EM CENTAVOS
        /// REQUIRED | BOLETO MIN 300
        /// </summary>
        /// <value></value>

        [JsonProperty("amount")]
        public long? Amount { get; set; }
        /// <summary>
        /// MOEDA
        /// USE ENUM CurrencyType
        /// </summary>
        /// <value></value>
        [JsonProperty("currency")]
        public CurrencyType Currency { get; set; }
        /// <summary>
        /// DESCRIÇÃO
        /// REQUIRED
        /// </summary>
        /// <value></value>

        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// TIPO DE PAGAMENTO
        /// REQUIRED
        /// </summary>
        /// <value></value>

        [JsonProperty("payment_type")]
        public PaymentType PaymentType { get; set; }
        /// <summary>
        /// CAPTURAR OU VALIDAR SALDO - PENDENTE SOLICITAÇÃO DE CAPTURA DE COBRANÇA
        /// REQUIRED
        /// </summary>
        /// <value></value>
        [JsonProperty("capture")]
        public bool Capture { get; set; }
        /// <summary>
        /// ID DO VENDERDOR  - MARKET PLACE 
        /// REQUIRED
        /// </summary>
        /// <value></value>
        [JsonProperty("on_behalf_of")]
        [Required(ErrorMessage = "Informe o id do vendedor")]
        public string OnBehalfOf { get; set; }
        /// <summary>
        /// REFERENCIA INTERNA 
        /// </summary>
        /// <value></value>
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
        /// <summary>
        /// CONFIGURAÇÕES DE PAGAMENTO
        /// </summary>
        /// <value></value>

        [JsonProperty("split_rules")]
        public List<SplitRulesViewModel> SplitRules { get; set; }
    }
}