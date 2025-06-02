using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Request
{
    public class TransactionsViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
