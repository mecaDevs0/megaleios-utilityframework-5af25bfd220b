using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Request
{
    public class TransactionsViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
