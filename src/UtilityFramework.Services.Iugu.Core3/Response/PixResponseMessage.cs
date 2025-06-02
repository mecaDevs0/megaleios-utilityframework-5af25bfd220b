using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class PixResponseMessage
    {
        [JsonProperty("qrcode")]
        public string Qrcode { get; set; }

        [JsonProperty("qrcode_text")]
        public string QrcodeText { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payer_cpf_cnpj")]
        public string PayerCpfCnpj { get; set; }

        [JsonProperty("payer_name")]
        public string PayerName { get; set; }

        [JsonProperty("end_to_end_id")]
        public string EndToEndId { get; set; }

        [JsonProperty("end_to_end_refund_id")]
        public string EndToEndRefundId { get; set; }
    }
}