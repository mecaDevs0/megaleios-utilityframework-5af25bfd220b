using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class CardViewModel : RegisterCreditCardViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("card_brand")]
        public string CardBrand { get; set; }

        [JsonProperty("first4_digits")]
        public long First4Digits { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("address")]
        public RegisterAddressViewModel Address { get; set; }

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

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }
}