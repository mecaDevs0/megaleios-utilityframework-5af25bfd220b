using System;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class ResponsePaymentMethodViewModel : BaseErrorViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty("expiration_date")]
        public DateTimeOffset? ExpirationDate { get; set; }

        [JsonProperty("payment_limit_date")]
        public object PaymentLimitDate { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("address")]
        public object Address { get; set; }

        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("accepted")]
        public bool Accepted { get; set; }

        [JsonProperty("printed")]
        public bool Printed { get; set; }

        [JsonProperty("downloaded")]
        public bool Downloaded { get; set; }

        [JsonProperty("fingerprint")]
        public object Fingerprint { get; set; }

        [JsonProperty("paid_at")]
        public object PaidAt { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

}