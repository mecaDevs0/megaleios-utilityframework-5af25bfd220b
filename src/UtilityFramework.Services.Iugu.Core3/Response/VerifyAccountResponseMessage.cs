using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Entity;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    /// <summary>
    /// Resposta da verificação dos dados de uma conta
    /// </summary>
    public class VerifyAccountResponseMessage
    {
        /// <summary>
        /// Identificação da verificação
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Identificação da conta
        /// </summary>
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Dados da conta
        /// </summary>
        [JsonProperty("data")]
        public AccountModel Data { get; set; }

        /// <summary>
        /// Data da criação da verificação
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
