using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class BankSlipTransactionViewModel : BaseTransactionViewModel
    {

        [JsonProperty("customer")]
        public string Customer { get; set; }

    }
}