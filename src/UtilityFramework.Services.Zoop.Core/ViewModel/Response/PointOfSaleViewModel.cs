using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class PointOfSaleViewModel
    {

        [JsonProperty("entry_mode")]
        public string EntryMode { get; set; }

        [JsonProperty("identification_number")]
        public object IdentificationNumber { get; set; }
    }

}