using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{

    public class RegisterCreditCardTransactionViewModel : BaseTransactionViewModel
    {
        /// <summary>
        /// DADOS DO CARTÃO PARA TRANSAÇÃO
        /// {
        ///     "amount": 100, // VALOR COBRADO EM CENTAVOS
        ///     "currency": "BRL", // MOEADA
        ///     "usage": "single_use", // ENVIAR SINGLE_USAGE
        ///     "type": "card", //ENVIAR CARD
        ///     "capture": true, // COBRAR OU VALIDAR SALDO
        ///     "card": {
        ///         "id": "" //ID DO CARTÃO
        ///      }
        /// }
        /// 
        /// </summary>
        /// <value></value>
        [JsonProperty("source")]
        public SourceViewModel Source { get; set; }
    }

}