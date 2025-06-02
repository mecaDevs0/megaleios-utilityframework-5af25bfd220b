using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class BankSlipResponseModel
    {

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("extra_due")]
        public string ExtraDue { get; set; }

        [JsonProperty("reprint_extra_due")]
        public string ReprintExtraDue { get; set; }
    }



}