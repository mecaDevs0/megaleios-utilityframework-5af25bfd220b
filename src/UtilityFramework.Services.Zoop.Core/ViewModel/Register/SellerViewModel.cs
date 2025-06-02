using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class SellerViewModel : BuyerViewModel
    {
        
        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("account_balance")]
        public string AccountBalance { get; set; }

        [JsonProperty("current_balance")]
        public string CurrentBalance { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("business_phone")]
        public string BusinessPhone { get; set; }

        [JsonProperty("business_email")]
        public string BusinessEmail { get; set; }

        [JsonProperty("business_website")]
        public string BusinessWebsite { get; set; }

        [JsonProperty("business_description")]
        public string BusinessDescription { get; set; }

        [JsonProperty("business_opening_date")]
        public string BusinessOpeningDate { get; set; }

        [JsonProperty("business_facebook")]
        public string BusinessFacebook { get; set; }

        [JsonProperty("business_twitter")]
        public object BusinessTwitter { get; set; }

        [JsonProperty("ein")]
        public string Ein { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("mcc")]
        public string Mcc { get; set; }

        [JsonProperty("business_address")]
        public RegisterAddressViewModel BusinessAddress { get; set; }

        [JsonProperty("owner")]
        public BuyerViewModel Owner { get; set; }

        [JsonProperty("show_profile_online")]
        public bool ShowProfileOnline { get; set; }

        [JsonProperty("is_mobile")]
        public bool IsMobile { get; set; }

        [JsonProperty("decline_on_fail_security_code")]
        public bool DeclineOnFailSecurityCode { get; set; }

        [JsonProperty("decline_on_fail_zipcode")]
        public bool DeclineOnFailZipcode { get; set; }

        [JsonProperty("delinquent")]
        public bool Delinquent { get; set; }

        [JsonProperty("payment_methods")]
        public string PaymentMethods { get; set; }

        [JsonProperty("default_debit")]
        public string DefaultDebit { get; set; }

        [JsonProperty("default_credit")]
        public string DefaultCredit { get; set; }

        [JsonProperty("merchant_code")]
        public string MerchantCode { get; set; }

        [JsonProperty("terminal_code")]
        public string TerminalCode { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("marketplace_id")]
        public string MarketplaceId { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// TIPO DE VENDEDOR
        /// </summary>
        /// <value></value>
        [JsonProperty("type")]
        public TypeSeller Type { get; set; }
        /// <summary>
        /// ID de vendedor responsável por receber os créditos pelas vendas
        /// </summary>
        /// <value></value>

        [JsonProperty("fiscal_responsibility")]
        public string FiscalResponsibility { get; set; }

        [JsonProperty("owner_address")]
        public RegisterAddressViewModel OwnerAddress { get; set; }
    }

}