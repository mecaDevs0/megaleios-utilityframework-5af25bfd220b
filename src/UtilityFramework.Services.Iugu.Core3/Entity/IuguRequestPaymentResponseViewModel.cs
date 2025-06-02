using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class IuguRequestPaymentResponseViewModel : IuguBaseErrors
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("document_amount_cents")]
        public int DocumentAmountCents { get; set; }

        [JsonProperty("amount_cents")]
        public int AmountCents { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}