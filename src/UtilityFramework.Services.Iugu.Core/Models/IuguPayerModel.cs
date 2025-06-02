using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguPayerModel
    {
        [JsonProperty("cpf_cnpj")]
        public string CpfOrCnpj { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_prefix")]
        public string PhonePrefix { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("address")]
        public IuguAddressModel Address { get; set; }
    }
}