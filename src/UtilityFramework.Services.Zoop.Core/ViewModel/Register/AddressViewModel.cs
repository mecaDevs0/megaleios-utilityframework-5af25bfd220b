using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterAddressViewModel
    {
        public RegisterAddressViewModel()
        {
            CountryCode = "BR";
        }
        /// <summary>
        /// RUA
        /// </summary>
        /// <value></value>
        [JsonProperty("line1")]
        [Required(ErrorMessage = "Informe o campo endereço")]
        public string Line1 { get; set; }

        /// <summary>
        /// NUMERO
        /// </summary>
        /// <value></value>
        [JsonProperty("line2")]
        public string Line2 { get; set; }
        /// <summary>
        ///  COMPLEMENTO
        /// </summary>
        /// <value></value>
        [JsonProperty("line3")]
        public string Line3 { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        /// <summary>
        /// CIDADE 
        /// </summary>
        /// <value></value>
        [JsonProperty("city")]
        [Required(ErrorMessage = "Informe o campo cidade")]
        public string City { get; set; }

        /// <summary>
        /// Código ISO 3166-2 para o estado = EX:SP
        /// </summary>
        /// <value></value>
        [JsonProperty("state")]
        [Required(ErrorMessage = "Informe o campo estado")]
        public string State { get; set; }

        /// <summary>
        /// #####-###
        /// </summary>
        /// <value></value>
        [JsonProperty("postal_code")]
        [Required(ErrorMessage = "Informe o campo Cep")]
        public string PostalCode { get; set; }
        /// <summary>
        /// ISO 3166-1 alpha-2 - códigos de país de duas letras. = BR
        ///         /// </summary>
        /// <value></value>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}