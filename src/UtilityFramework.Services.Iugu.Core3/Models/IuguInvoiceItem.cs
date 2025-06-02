using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguInvoiceItem
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price_cents")]
        public int PriceCents { get; set; }
    }
}