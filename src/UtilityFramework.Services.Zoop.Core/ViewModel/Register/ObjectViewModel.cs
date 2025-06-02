using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class ObjectViewModel
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("original_amount")]
        public string OriginalAmount { get; set; }

        [JsonProperty("currency")]
        public CurrencyType Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("gateway_authorizer")]
        public string GatewayAuthorizer { get; set; }

        [JsonProperty("app_transaction_uid")]
        public object AppTransactionUid { get; set; }

        [JsonProperty("refunds")]
        public object Refunds { get; set; }

        [JsonProperty("rewards")]
        public object Rewards { get; set; }

        [JsonProperty("discounts")]
        public object Discounts { get; set; }

        [JsonProperty("pre_authorization")]
        public object PreAuthorization { get; set; }

        [JsonProperty("sales_receipt")]
        public string SalesReceipt { get; set; }

        [JsonProperty("on_behalf_of")]
        public string OnBehalfOf { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        [JsonProperty("payment_method")]
        public PaymentMethodViewModel PaymentMethod { get; set; }

        [JsonProperty("source")]
        public SourceViewModel Source { get; set; }

        [JsonProperty("point_of_sale")]
        public PointOfSaleViewModel PointOfSale { get; set; }

        [JsonProperty("installment_plan")]
        public object InstallmentPlan { get; set; }

        [JsonProperty("refunded")]
        public bool Refunded { get; set; }

        [JsonProperty("voided")]
        public bool Voided { get; set; }

        [JsonProperty("captured")]
        public bool Captured { get; set; }

        [JsonProperty("fees")]
        public string Fees { get; set; }

        [JsonProperty("fee_details")]
        public List<FeeDetailViewModel> FeeDetails { get; set; }

        [JsonProperty("location_latitude")]
        public object LocationLatitude { get; set; }

        [JsonProperty("location_longitude")]
        public object LocationLongitude { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("expected_on")]
        public DateTimeOffset? ExpectedOn { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("payment_authorization")]
        public PaymentAuthorization PaymentAuthorization { get; set; }

        [JsonProperty("history")]
        public List<History> History { get; set; }
    }
}