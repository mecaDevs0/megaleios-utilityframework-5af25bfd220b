using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    /// <summary>
    /// Modelo que contém informações de pagamento
    /// </summary>
    public class PaymentInfoModel
    {
        /// <summary>
        /// Número do Cartão de Crédito
        /// </summary>
        [JsonProperty("number")]
        [Display(Name = "Número do cartão")]

        public string Number { get; set; }

        /// <summary>
        /// CVV do Cartão de Crédito
        /// </summary>
        [JsonProperty("verification_value")]
        [Display(Name = "Código de verificação")]

        public string VerificationValue { get; set; }

        /// <summary>
        /// Nome do Cliente como está no Cartão
        /// </summary>
        [JsonProperty("first_name")]
        [Display(Name = "Primeiro nome como está no cartão")]

        public string FirstName { get; set; }

        /// <summary>
        /// Sobrenome do Cliente como está no Cartão
        /// </summary>
        [JsonProperty("last_name")]
        [Display(Name = "Sobrenome como está no cartão")]

        public string LastName { get; set; }

        /// <summary>
        /// Mês de Vencimento no Formato MM (Ex: 01, 02, 12)
        /// </summary>
        [JsonProperty("month")]
        [Display(Name = "Mês de vencimento")]

        public string Month { get; set; }

        /// <summary>
        /// Ano de Vencimento no Formato YYYY (2014, 2015, 2016)
        /// </summary>
        [JsonProperty("year")]
        [Display(Name = "Ano de vencimento")]

        public string Year { get; set; }
    }
}
