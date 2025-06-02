using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{

    public class GoogleRootViewModel
    {

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonIgnore]
        public bool HasError { get; set; }
    }



}