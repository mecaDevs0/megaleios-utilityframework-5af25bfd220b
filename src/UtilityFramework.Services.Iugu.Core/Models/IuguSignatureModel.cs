using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguSignatureModel : IuguBaseErrors
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }
        [JsonProperty("only_on_charge_success")]
        public bool OnlyOnChargeSuccess { get; set; }

        [JsonProperty("plan_identifier")]
        public string PlanIdentifier { get; set; }
        [JsonProperty("payable_with")]
        public string PayableWith { get; set; }

        [JsonProperty("price_cents")]
        public int PriceCents { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("features")]
        public FeaturesModel Features { get; set; }

        [JsonProperty("expires_at")]
        public object ExpiresAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_email")]
        public string CustomerEmail { get; set; }

        [JsonProperty("cycled_at")]
        public object CycledAt { get; set; }

        [JsonProperty("credits_min")]
        public int CreditsMin { get; set; }

        [JsonProperty("credits_cycle")]
        public object CreditsCycle { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("plan_name")]
        public string PlanName { get; set; }

        [JsonProperty("customer_ref")]
        public string CustomerRef { get; set; }

        [JsonProperty("plan_ref")]
        public string PlanRef { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("in_trial")]
        public object InTrial { get; set; }

        [JsonProperty("credits")]
        public int Credits { get; set; }

        [JsonProperty("credits_based")]
        public bool CreditsBased { get; set; }

        [JsonProperty("recent_invoices")]
        public object RecentInvoices { get; set; }

        [JsonProperty("subitems")]
        public List<Subitem> Subitems { get; set; }

        [JsonProperty("logs")]
        public List<LogModel> Logs { get; set; }

        [JsonProperty("custom_variables")]
        public List<object> CustomVariables { get; set; }
    }

    public class FeaturesModel
    {
        [JsonProperty("feat")]
        public FeatModel Feat { get; set; }
    }

    public class FeatModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Subitem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price_cents")]
        public int PriceCents { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }

    public class LogModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}