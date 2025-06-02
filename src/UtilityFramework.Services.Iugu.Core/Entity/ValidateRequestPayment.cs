using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class ValidateRequestPayment
    {
        /// <summary>
        /// Código de barras
        /// </summary>
        /// <value></value>
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        /// <summary>
        /// DETALHADO
        /// </summary>
        [JsonProperty("detailed")]
        public bool Detailed { get; set; } = true;

    }
}