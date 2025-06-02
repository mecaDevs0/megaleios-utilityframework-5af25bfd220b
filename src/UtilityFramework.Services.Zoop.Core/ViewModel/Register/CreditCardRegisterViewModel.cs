using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterCreditCardViewModel : BaseErrorViewModel
    {
        /// <summary>
        /// NOME NO CARTÃO 
        /// </summary>
        /// <value></value>
        [JsonProperty("holder_name")]
        public string HolderName { get; set; }
        /// <summary>
        /// MES QUE O CARTÃO EXPIRA MM
        /// </summary>
        /// <value></value>
        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }
        /// <summary>
        /// ANO QUE O CARTÃO EXPIRA AAAA
        /// </summary>
        /// <value></value>
        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }
        /// <summary>
        /// NUMERO DO CARTÃO DE CREDITO #### #### #### ####
        /// </summary>
        /// <value></value>
        [JsonProperty("card_number")]
        public string CardNumber { get; set; }
        /// <summary>
        /// CODIGO DE SEGURANÇA #### OU ###
        /// </summary>
        /// <value></value>
        [JsonProperty("security_code")]
        public string SecurityCode { get; set; }
    }
}