using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Request
{
    public class EarlyPaymentDiscountsModel
    {
        /// <summary>
        /// VALOR EM CENTAVOS DO DESCONTO
        /// </summary>
        /// <value></value>
        [JsonProperty("value_cents")]
        public int? ValueCents { get; set; }

        /// <summary>
        /// PORCENTAGEM DE DESCONTO
        /// </summary>
        /// <value></value>
        [JsonProperty("percent")]
        public int? Percent { get; set; }

        /// <summary>
        ///  DIAS ANTES DO VENCIMENTOS
        /// </summary>
        /// <value></value>
        [JsonProperty("days")]
        public int Days { get; set; }
    }
}