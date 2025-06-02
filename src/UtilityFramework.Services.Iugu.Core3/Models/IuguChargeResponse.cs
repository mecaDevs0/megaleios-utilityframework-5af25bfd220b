using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguChargeResponse : IuguBaseErrors
    {
        /// <summary>
        /// Url do boleto
        /// </summary>
        //[JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Informa se a cobrança foi gerada com sucesso
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("pdf")]
        public string Pdf { get; set; }
        [JsonProperty("identification")]
        public object Identification { get; set; }
        /// <summary>
        /// Número da fatura da cobrança
        /// </summary>
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Mensagem de resposta
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("LR")]
        public string LR { get; set; }

        public string MsgLR { get; set; }

    }


}