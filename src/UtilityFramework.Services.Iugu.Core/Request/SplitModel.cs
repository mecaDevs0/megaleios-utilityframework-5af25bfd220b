using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Request
{
    public class SplitModel
    {

        /// <summary>
        /// ID Da conta que irá receber o split
        /// </summary>
        [JsonProperty("recipient_account_id")]
        public string RecipientAccountId { get; set; }
        /// <summary>
        /// Centavos a serem cobrados da fatura
        /// </summary>
        [JsonProperty("cents")]
        public int? Cents { get; set; }
        /// <summary>
        /// Porcentagem a ser cobrada da fatura
        /// </summary>
        [JsonProperty("percent")]
        public double? Percent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações no boleto.
        /// </summary>
        [JsonProperty("bank_slip_cents")]
        public int? BankSlipCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações no boleto.
        /// </summary>
        [JsonProperty("bank_slip_percent")]
        public double? BankSlipPercent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_cents")]
        public int? CreditCardCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_percent")]
        public double? CreditCardPercent { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações no pix.
        /// </summary>
        [JsonProperty("pix_cents")]
        public int? PixCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações no pix.
        /// </summary>
        [JsonProperty("pix_percent")]
        public double? PixPercent { get; set; }
        /// <summary>
        /// Permite agregar comissionamento percentual + fixo.
        /// </summary>
        [JsonProperty("permit_aggregated")]
        public bool PermitAggregated { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 1x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_1x_cents")]
        public int? CreditCard1xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 2x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_2x_cents")]
        public int? CreditCard2xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 3x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_3x_cents")]
        public int? CreditCard3xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 4x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_4x_cents")]
        public int? CreditCard4xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 5x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_5x_cents")]
        public int? CreditCard5xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 6x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_6x_cents")]
        public int? CreditCard6xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 7x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_7x_cents")]
        public int? CreditCard7xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 8x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_8x_cents")]
        public int? CreditCard8xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 9x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_9x_cents")]
        public int? CreditCard9xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 10x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_10x_cents")]
        public int? CreditCard10xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 11x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_11x_cents")]
        public int? CreditCard11xCents { get; set; }
        /// <summary>
        /// Valor em centavos a ser cobrado apenas em transações em 12x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_12x_cents")]
        public int? CreditCard12xCents { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 1x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_1x_percent")]
        public double? CreditCard1xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 2x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_2x_percent")]
        public double? CreditCard2xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 3x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_3x_percent")]
        public double? CreditCard3xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 4x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_4x_percent")]
        public double? CreditCard4xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 5x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_5x_percent")]
        public double? CreditCard5xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 6x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_6x_percent")]
        public double? CreditCard6xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 7x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_7x_percent")]
        public double? CreditCard7xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 8x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_8x_percent")]
        public double? CreditCard8xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 9x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_9x_percent")]
        public double? CreditCard9xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 10x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_10x_percent")]
        public double? CreditCard10xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 11x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_11x_percent")]
        public double? CreditCard11xPercent { get; set; }
        /// <summary>
        /// Valor em porcentagem a ser cobrado apenas em transações em 12x no cartão de crédito.
        /// </summary>
        [JsonProperty("credit_card_12x_percent")]
        public double? CreditCard12xPercent { get; set; }
    }
}
