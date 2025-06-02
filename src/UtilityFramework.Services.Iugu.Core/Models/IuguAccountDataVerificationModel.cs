using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAccountDataVerificationModel : IuguBaseErrors
    {
        public IuguAccountDataVerificationModel(bool setDefault = true)
        {
            if (setDefault)
            {
                AutomaticValidation = true;
                AutomaticTransfer = false;
                PersonType = "Pessoa Física";
                AccountType = "Corrente";
                PhysicalProducts = false;
                PriceRange = "Mais que R$ 1,00";
                BusinessType = "Prestação de serviços";
            }

        }
        /// <summary>
        /// Validação automática
        /// </summary>
        [Display(Name = "Validação automática")]
        [JsonProperty("automatic_validation")]
        public bool AutomaticValidation { get; set; }
        /// <summary>
        /// Valor máximo da venda
        /// </summary>
        [Display(Name = "Valor máximo da venda")]
        [JsonProperty("price_range")]
        public string PriceRange { get; set; }
        /// <summary>
        /// Produtos físicos
        /// </summary>
        [Display(Name = "Produtos físicos")]
        [JsonProperty("physical_products")]
        public bool PhysicalProducts { get; set; }
        /// <summary>
        /// RAMO DA EMPRESA
        /// </summary>

        [Display(Name = "Ramo da empresa")]
        [JsonProperty("business_type")]
        public string BusinessType { get; set; }
        /// <summary>
        /// Pessoa Física / Pessoa Jurídica
        /// </summary>
        [Display(Name = "Tipo de pessoa")]
        [JsonProperty("person_type")]
        public string PersonType { get; set; }
        /// <summary>
        /// Transferência automática
        /// </summary>
        [Display(Name = "Transferência automática")]
        [JsonProperty("automatic_transfer")]
        public bool AutomaticTransfer { get; set; }
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        /// <summary>
        /// Nome da empresa
        /// </summary>
        [Display(Name = "Nome da empresa")]
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [Display(Name = "Nome")]
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Endereço
        /// </summary>
        [Display(Name = "Endereço")]
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        [Display(Name = "Cidade")]
        [JsonProperty("city")]
        public string City { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [Display(Name = "Estado")]
        [JsonProperty("state")]
        public string State { get; set; }
        /// <summary>
        /// Telefone
        /// </summary>
        [Display(Name = "Telefone")]
        [JsonProperty("telphone")]
        public string Telphone { get; set; }
        /// <summary>
        /// Responsável
        /// </summary>
        [Display(Name = "Responsável")]
        [JsonProperty("resp_name")]
        public string RespName { get; set; }
        /// <summary>
        /// Cpf Responsável
        /// </summary>
        [Display(Name = "Cpf Responsável")]
        [JsonProperty("resp_cpf")]
        public string RespCpf { get; set; }
        /// <summary>
        /// Banco
        /// </summary>
        [Display(Name = "Banco")]
        [JsonProperty("bank")]
        public string Bank { get; set; }
        /// <summary>
        /// Agência
        /// </summary>
        [Display(Name = "Agência")]
        [JsonProperty("bank_ag")]
        public string BankAg { get; set; }
        /// <summary>
        /// Agência (ignorar)
        /// </summary>
        [Display(Name = "Agência")]
        [JsonProperty("agency")]
        public string AgencyIgnored { get; set; }

        /// <summary>
        /// Tipo de conta (CORRENTE  / POUPANÇA)
        /// </summary>
        [Display(Name = "Tipo de conta")]
        [JsonProperty("account_type")]
        public string AccountType { get; set; }
        /// <summary>
        /// Conta
        /// </summary>
        [Display(Name = "Conta")]
        [JsonProperty("bank_cc")]
        public string BankCc { get; set; }
    }
}
