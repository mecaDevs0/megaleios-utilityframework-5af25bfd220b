using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class BankAccountResponseModel
    {

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("digit")]
        public string Digit { get; set; }
    }



}