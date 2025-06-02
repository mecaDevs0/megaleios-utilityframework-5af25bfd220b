using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class DuplicateViewModel
    {

        [JsonProperty("items")]
        public List<Item> Items { get; set; } = new List<Item>();

        [JsonProperty("keep_early_payment_discount")]
        public bool KeepEarlyPaymentDiscount { get; set; }

        [JsonProperty("current_fines_option")]
        public bool CurrentFinesOption { get; set; }

        [JsonProperty("ignore_canceled_email")]
        public bool IgnoreCanceledEmail { get; set; }

        [JsonProperty("ignore_due_email")]
        public bool IgnoreDueEmail { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }
    }

}
