using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class ItemViewModel : BuyerViewModel
    {
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("account_balance")]
        public double AccountBalance { get; set; }

        [JsonProperty("current_balance")]
        public double CurrentBalance { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }
        [JsonProperty("delinquent")]
        public bool Delinquent { get; set; }

        [JsonProperty("payment_methods")]
        public object PaymentMethods { get; set; }

        [JsonProperty("default_debit")]
        public string DefaultDebit { get; set; }

        [JsonProperty("default_credit")]
        public string DefaultCredit { get; set; }

        [JsonProperty("default_receipt_delivery_method")]
        public string DefaultReceiptDeliveryMethod { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}