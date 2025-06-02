using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class BankSlipResponseMessage
    {
        [JsonProperty("digitable_line")]
        public string DigitableLine { get; set; }
        [JsonProperty("barcode_data")]
        public string BarcodeData { get; set; }
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        [JsonProperty("bank_slip_bank")]
        public int BankSlipBank { get; set; }
        [JsonProperty("bank_slip_status")]
        public string BankSlipStatus { get; set; }
        [JsonProperty("bank_slip_error_code")]
        public string BankSlipErrorCode { get; set; }
        [JsonProperty("bank_slip_error_message")]
        public string BankSlipErrorMessage { get; set; }

    }
}