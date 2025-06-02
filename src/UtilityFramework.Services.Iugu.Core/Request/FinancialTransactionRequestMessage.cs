using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Request
{
    /// <summary>
    /// Requisição para a API de contas
    /// </summary>
    public class FinancialTransactionRequestMessage
    {
        /// <summary>
        ///  Variáveis Personalizadas
        /// </summary>
        [JsonProperty("transactions")]
        public List<TransactionsViewModel> Transactions { get; set; }

    }
}
