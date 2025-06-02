using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Iugu.Core.Models
{

    /// <summary>
    /// FORMA DE PAGAMENTO
    /// </summary>
    public class IuguCreditCard : IuguBaseErrors
    {

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("item_type")]
        public string ItemType { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    /// <summary>
    /// DADOS DO CARTÃO
    /// </summary>
    public class Data
    {
        [JsonProperty("holder_name")]
        [Display(Name = "Nome no cartão")]

        public string HolderName { get; set; }
        [JsonProperty("display_number")]
        [Display(Name = "Número do cartão")]

        public string DisplayNumber { get; set; }
        [JsonProperty("brand")]
        [Display(Name = "Bandeira")]

        public string Brand { get; set; }
        [JsonProperty("month")]
        [Display(Name = "Mês de vencimento")]

        public int Month { get; set; }
        [JsonProperty("year")]
        [Display(Name = "Ano de vencimento")]

        public int Year { get; set; }
    }

}
