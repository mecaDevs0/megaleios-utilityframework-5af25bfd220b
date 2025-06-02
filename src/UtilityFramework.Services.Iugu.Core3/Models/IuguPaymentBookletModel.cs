using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Entity;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class IuguPaymentBookletModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("customer")]
        public CustomerModel Customer { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price_cents")]
        public int PriceCents { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("finish_at")]
        public DateTime? FinishAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payment_booklet_installments")]
        public int PaymentBookletInstallments { get; set; }
    }
}