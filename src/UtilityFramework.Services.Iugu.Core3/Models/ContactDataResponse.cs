using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class ContactDataResponseModel
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty("full_address")]
        public string FullAddress { get; set; }
    }



}