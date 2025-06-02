using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class SplitRulesViewModel
    {
        /// <summary>
        /// Identificador do vendedor recebedor
        /// </summary>
        /// <value></value>
        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// define se o recebedor arca com prejuízo em caso de chargeback ou não. 1 arca; 0 não arca.
        /// </summary>
        /// <value></value>
        [JsonProperty("liable")]
        public bool Liable { get; set; }
        /// <summary>
        /// define se vai ser feito split em cima do valor bruto (0) ou do valor líquido (1) da transação
        /// </summary>
        /// <value></value>

        [JsonProperty("charge_processing_fee")]
        public bool ChargeProcessingFee { get; set; }
        /// <summary>
        /// PORCENTAGEM DE COMISSÃO
        /// </summary>
        /// <value></value>

        [JsonProperty("percentage")]
        public int? Percentage { get; set; }
        /// <summary>
        /// valor em centavos a ser splitado.
        /// </summary>
        /// <value></value>
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}