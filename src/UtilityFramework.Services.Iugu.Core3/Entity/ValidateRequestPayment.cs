using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class ValidateRequestPayment
    {
        /// <summary>
        /// Código de barras
        /// </summary>
        [Display(Name = "Código de barras")]
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        /// <summary>
        /// Detalhado
        /// </summary>
        [Display(Name = "Detalhado")]
        [JsonProperty("detailed")]
        public bool Detailed { get; set; } = true;
    }
}