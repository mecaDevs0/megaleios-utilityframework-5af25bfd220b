using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Request
{
    public class IuguUpdateDataBank : IuguBaseErrors
    {
        /// <summary>
        /// CODIGO DO BANCO
        /// </summary>
        [JsonProperty("bank")]
        [Display(Name = "Banco")]
        public string Bank { get; set; }

        /// <summary>
        /// TIPO DE CONTA CC / CP
        /// </summary>
        [JsonProperty("account_type")]
        [Display(Name = "Tipo de conta")]

        public string AccountType { get; set; }
        /// <summary>
        /// Conta
        /// </summary>
        [JsonProperty("account")]
        [Display(Name = "Conta")]
        public string Account { get; set; }
        /// <summary>
        /// Agência
        /// </summary>
        [JsonProperty("agency")]
        [Display(Name = "Agência")]
        public string Agency { get; set; }

        /// <summary>
        /// Validação automática
        /// </summary>
        [JsonProperty("automatic_validation")]
        [Display(Name = "Validação automática")]
        public string AutomaticValidation { get; set; }
    }
}