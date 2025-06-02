using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Iugu.Core.Entity
{

    public class Logs
    {
        /// <summary>
        /// Descrição da Entrada de Log
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Anotações da Entrada de Log
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }
    }

    public class Feature
    {
        /// <summary>
        /// Nome da Funcionalidade
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Identificador único da funcionalidade
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Valor da Funcionalidade (número maior que 0)
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Prices
    {
        /// <summary>
        /// Moeda do Preço (Somente "BRL" por enquanto)
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Preço do Plano em Centavos
        /// </summary>
        [JsonProperty("value_cents")]
        public int ValueCents { get; set; }
    }

    public class CreditCard
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
