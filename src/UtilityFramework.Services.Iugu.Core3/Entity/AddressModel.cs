using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    /// <summary>
    /// Model que representa dados de endereço
    /// </summary>
    public class AddressModel
    {
        /// <summary>
        /// Rua
        /// </summary>
        [JsonProperty("street")]
        [Display(Name = "Rua")]

        public string Street { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        [JsonProperty("number")]
        [Display(Name = "Número")]

        public string Number { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [JsonProperty("city")]
        [Display(Name = "Cidade")]

        public string City { get; set; }

        /// <summary>
        /// Estado (Ex: SP)
        /// </summary>
        [JsonProperty("state")]
        [Display(Name = "Estado")]

        public string State { get; set; }

        /// <summary>
        /// País
        /// </summary>
        [JsonProperty("country")]
        [Display(Name = "País")]

        public string Country { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [JsonProperty("zip_code")]
        [Display(Name = "Cep")]

        public string ZipCode { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        [JsonProperty("district")]
        [Display(Name = "Bairro")]
        public string District { get; set; }
        /// <summary>
        /// Complemento
        /// </summary>
        [JsonProperty("complement")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }
    }
}
