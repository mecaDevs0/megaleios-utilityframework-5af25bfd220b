using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{



    public class VerificationChecklist
    {

        [JsonProperty("postal_code_check")]
        public string PostalCodeCheck { get; set; }

        [JsonProperty("security_code_check")]
        public string SecurityCodeCheck { get; set; }

        [JsonProperty("address_line1_check")]
        public string AddressLine1Check { get; set; }
    }

    public class Metadata
    {
    }

    public class PaymentMethod
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
        public string First4Digits { get; set; }

        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }

        [JsonProperty("holder_name")]
        public string HolderName { get; set; }

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
        public object Address { get; set; }

        [JsonProperty("verification_checklist")]
        public VerificationChecklist VerificationChecklist { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public class Card
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
        public string First4Digits { get; set; }

        [JsonProperty("expiration_month")]
        public string ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public string ExpirationYear { get; set; }

        [JsonProperty("holder_name")]
        public string HolderName { get; set; }

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
        public object Address { get; set; }

        [JsonProperty("verification_checklist")]
        public VerificationChecklist VerificationChecklist { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class Source
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("statement_descriptor")]
        public object StatementDescriptor { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }
    }

    public class PointOfSale
    {

        [JsonProperty("entry_mode")]
        public string EntryMode { get; set; }

        [JsonProperty("identification_number")]
        public object IdentificationNumber { get; set; }
    }

    public class FeeDetail
    {

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("prepaid")]
        public bool Prepaid { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("is_gateway_fee")]
        public bool IsGatewayFee { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class PaymentAuthorization
    {

        [JsonProperty("authorizer_id")]
        public string AuthorizerId { get; set; }

        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("authorization_nsu")]
        public string AuthorizationNsu { get; set; }
    }

    public class History
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("transaction")]
        public string Transaction { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("operation_type")]
        public string OperationType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        [JsonProperty("response_message")]
        public object ResponseMessage { get; set; }

        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("authorizer_id")]
        public string AuthorizerId { get; set; }

        [JsonProperty("authorization_nsu")]
        public string AuthorizationNsu { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public class Example
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
        public string Currency { get; set; }

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
        public PaymentMethod PaymentMethod { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("point_of_sale")]
        public PointOfSale PointOfSale { get; set; }

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
        public List<FeeDetail> FeeDetails { get; set; }

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