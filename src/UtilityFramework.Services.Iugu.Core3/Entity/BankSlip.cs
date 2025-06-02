using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class BankSlip
    {
        /// <summary>
        /// Linha digitável
        /// </summary>
        [JsonProperty("digitable_line")]
        public string DigitableLine { get; set; }

        /// <summary>
        /// Dados do código de barras
        /// </summary>
        [JsonProperty("barcode_data")]
        public string BarcodeData { get; set; }

        /// <summary>
        /// Código de barras
        /// </summary>
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
    }
}
