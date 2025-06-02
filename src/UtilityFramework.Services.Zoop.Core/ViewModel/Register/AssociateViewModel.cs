using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class AssociateViewModel
    {
        public AssociateViewModel(string token = null, string customer = null)
        {
            Token = token;
            Customer = customer;
        }
        [JsonProperty("token")]

        public string Token { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }
    }

}