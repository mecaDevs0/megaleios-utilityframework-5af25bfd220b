using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class IuguRequestPaymentViewModel
    {

        /// <summary>
        /// Código de barras do pagamento
        /// </summary>
        [Display(Name = "Código de barras do pagamento")]
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        /// <summary>
        /// Valor de pagamento do boleto
        /// </summary>
        [Display(Name = "Valor de pagamento do boleto")]
        [JsonProperty("amount_cents", NullValueHandling = NullValueHandling.Ignore)]
        public int? AmountCents { get; set; }
        /// <summary>
        /// Valor total do boleto
        /// </summary>
        [Display(Name = "Valor total do boleto")]
        [JsonProperty("document_amount_cents", NullValueHandling = NullValueHandling.Ignore)]
        public int? DocumentAmountCents { get; set; }
        /// <summary>
        /// Descrição do pagamento
        /// </summary>
        [Display(Name = "Descrição do pagamento")]
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// Chave privada
        /// </summary>
        [Display(Name = "Chave privada")]
        [JsonProperty("api_token", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiToken { get; set; }
    }
}