using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class CreditCardResponseModel
    {

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("soft_descriptor")]
        public string SoftDescriptor { get; set; }

        [JsonProperty("installments")]
        public bool? Installments { get; set; }

        [JsonProperty("installments_pass_interest")]
        public bool? InstallmentsPassInterest { get; set; }

        [JsonProperty("max_installments")]
        public string MaxInstallments { get; set; }

        [JsonProperty("max_installments_without_interest")]
        public string MaxInstallmentsWithoutInterest { get; set; }

        [JsonProperty("two_step_transaction")]
        public bool? TwoStepTransaction { get; set; }
    }



}