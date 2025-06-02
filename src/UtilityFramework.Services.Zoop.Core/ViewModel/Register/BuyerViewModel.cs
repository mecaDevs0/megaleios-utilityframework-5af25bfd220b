using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class BuyerViewModel : BaseErrorViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public StatusRegister? Status { get; set; }
        [JsonProperty("first_name")]
        [Required(ErrorMessage = "Informe o campo nome")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "Informe o campo e-mail")]
        public string Email { get; set; }
        /// <summary>
        /// CPF / CNPJ | APENAS NUMEROS
        /// </summary>
        /// <value></value>
        [JsonProperty("taxpayer_id")]
        [Required(ErrorMessage = "Informe o cpf ou cnpj")]
        public string TaxpayerId { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("address")]
        public RegisterAddressViewModel Address { get; set; }
    }
}