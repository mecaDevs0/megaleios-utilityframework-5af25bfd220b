using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterTokenTransactionViewModel : BaseTransactionViewModel
    {

        public RegisterTokenTransactionViewModel()
        {
            Currency = CurrencyType.BRL;
            PaymentType = PaymentType.Credit;
        }
        /// <summary>
        /// TOKEN DO CARTÃO 
        /// </summary>
        /// <value></value>
        [JsonProperty("token")]
        public string Token { get; set; }
        /// <summary>
        /// TOKEN DO CARTÃO 
        /// </summary>
        /// <value></value>
        [JsonProperty("customer")]
        public string Customer { get; set; }
    }

}