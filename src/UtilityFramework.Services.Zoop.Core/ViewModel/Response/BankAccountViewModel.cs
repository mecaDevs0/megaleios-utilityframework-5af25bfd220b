using System;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{

    public class BankAccountViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("holder_name")]
        public string HolderName { get; set; }

        /// <summary>
        /// CPF OU CNPJ
        /// </summary>
        /// <value></value>
        [JsonProperty("taxpayer_id")]
        public string TaxpayerId { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        /// <summary>
        /// NOME DO BANCO
        /// </summary>
        /// <value></value>
        [JsonProperty("bank_name")]
        public string BankName { get; set; }
        /// <summary>
        /// CODIGO DO BANCO 
        /// </summary>
        /// <value></value>

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("last4_digits")]
        public string Last4Digits { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("phone_number")]
        public object PhoneNumber { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("debitable")]
        public bool Debitable { get; set; }

        [JsonProperty("customer")]
        public object Customer { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("address")]
        public object Address { get; set; }

        [JsonProperty("verification_checklist")]
        public VerificationChecklistViewModel VerificationChecklist { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}