﻿using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Entity;

namespace UtilityFramework.Services.Iugu.Core3.Request
{
    public class PaymentTokenRequestMessage
    {
        /// <summary>
        /// ID de sua Conta na Iugu (O ID de sua conta pode ser encontrado clicando na referência)
        /// <see cref="https://iugu.com/settings/account"/>
        /// </summary>
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Método de Pagamento (atualmente somente credit_card)
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// Valor true para criar tokens de teste
        /// </summary>
        [JsonProperty("test")]
        public bool Test { get; set; }

        /// <summary>
        /// Dados do Método de Pagamento
        /// </summary>
        [JsonProperty("data")]
        public PaymentInfoModel PaymentData { get; set; }
    }
}
