using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Request
{
    public class CommissionModel
    {
        /// <summary>
        /// Valor em centavos
        /// </summary>
        [JsonProperty("cents")]
        public int Cents { get; set; }
        /// <summary>
        /// Valor em porcentagem.
        /// </summary>
        [JsonProperty("percent")]
        public double Percent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações no cartão de crédito. Precisa do parâmetro cents acima configurado.
        /// </summary>
        [JsonProperty("credit_card_cents")]
        public int CreditCardCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações no cartão de crédito. Precisa do parâmetro percent acima configurado.
        /// </summary>
        [JsonProperty("credit_card_percent")]
        public double CreditCardPercent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações no boleto. Precisa do parâmetro cents acima configurado.
        /// </summary>
        [JsonProperty("bank_slip_cents")]
        public int BankSlipCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações no boleto. Precisa do parâmetro percent acima configurado.
        /// </summary>
        [JsonProperty("bank_slip_percent")]
        public double BankSlipPercent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações via Pix. Precisa do parâmetro cents acima configurado.
        /// </summary>
        [JsonProperty("pix_cents")]
        public int PixCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações via Pix. Precisa do parâmetro percent acima configurado.
        /// </summary>
        [JsonProperty("pix_percent")]
        public double PixPercent { get; set; }
        /// <summary>
        /// Permite agregar comissionamento percentual e fixo
        /// </summary>
        [JsonProperty("permit_aggregated")]
        public bool PermitAggregated { get; set; }
    }
}
