using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UtilityFramework.Services.Zoop.Core.Enum
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeSeller
    {
        [EnumMember(Value = "individual")]
        Individual,
        [EnumMember(Value = "business")]
        Business,
    }



    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransferInterval
    {
        [EnumMember(Value = "daily")]
        Daily,
        [EnumMember(Value = "weekly")]
        Weekly,
        [EnumMember(Value = "monthly")]
        Monthly

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusRegister
    {
        [EnumMember(Value = "new")]
        New,
        [EnumMember(Value = "expired")]
        Expired,
        [EnumMember(Value = "action_required")]
        ActionRequired,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "enabled")]
        Enabled,
        [EnumMember(Value = "active")]
        Active,
        [EnumMember(Value = "disabled")]
        Disabled,
        [EnumMember(Value = "deleted")]
        Deleted
    }


    //[JsonConverter(typeof(StringEnumConverter))]
    //public enum TypeTokenEnum
    //{
    //    [EnumMember(Value = "card")]
    //    Card,
    //    [EnumMember(Value = "bankaccount")]
    //    BankAccount
    //}

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CategoryError
    {
        None,
        [EnumMember(Value = "server_api_error")]
        ServerApiError,
        [EnumMember(Value = "duplicate_taxpayer_id")]
        DuplicateTaxpayerId,
        [EnumMember(Value = "service_request_timeout")]
        RequestTimeout,
        [EnumMember(Value = "endpoint_not_found")]
        NotFound,
        [EnumMember(Value = "authentication_failed")]
        UnAuthorized,
        [EnumMember(Value = "expired_security_key")]
        ExpiredSecurityKey,
        [EnumMember(Value = "invalid_key_for_api_call")]
        InvalidKey,
        [EnumMember(Value = "transaction_amount_error")]
        TransactionAmountError,
        [EnumMember(Value = "transfer_amount_error")]
        TransferAmountError,
        [EnumMember(Value = "missing_required_param")]
        MissingRequiredParam,
        [EnumMember(Value = "unsupported_payment_type")]
        UnSupportedPaymentType,
        [EnumMember(Value = "invalid_payment_information")]
        InvalidPaymentInformation,
        [EnumMember(Value = "invalid_parameter")]
        InvalidParameter,
        [EnumMember(Value = "file_size_too_large")]
        FileLarge,
        [EnumMember(Value = "insufficient_escrow_funds_error")]
        InsufficientEscrowFunds,
        [EnumMember(Value = "capture_transaction_error")]
        CaptureTransactionError,
        [EnumMember(Value = "no_action_taken")]
        NoActionTaken,
        [EnumMember(Value = "seller_authorization_refused")]
        SellerAuthorizationRefused,
        [EnumMember(Value = "void_transaction_error")]
        VoidTransactionError,
        [EnumMember(Value = "invalid_expiry_month")]
        InvalidExpiryMonth,
        [EnumMember(Value = "invalid_expiry_year")]
        InvalidExpiryYear,
        [EnumMember(Value = "card_customer_not_associated")]
        CardCustomerNotAssociated,
        [EnumMember(Value = "insufficient_funds_error")]
        InsufficientFundsError,
        [EnumMember(Value = "expired_card_error")]
        ExpiredCardError,
        [EnumMember(Value = "invalid_card_number")]
        InvalidCardNumber,
        [EnumMember(Value = "invalid_pin_code")]
        InvalidPinCode,
        [EnumMember(Value = "authorization_refused")]
        AuthorizationRefused,
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeBankAccount
    {
        /// <summary>
        /// CONTA CORRENTE
        /// </summary>
        [EnumMember(Value = "checking")]
        Checking,
        /// <summary>
        /// CONTA POUPANÇA
        /// </summary>
        [EnumMember(Value = "savings")]
        Savings,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PaymentType
    {
        [EnumMember(Value = "credit")]
        Credit,
        [EnumMember(Value = "debit")]
        Debit,
        [EnumMember(Value = "wallet")]
        Wallet,
        [EnumMember(Value = "boleto")]
        Boleto
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionStatus
    {
        New,
        Pending,
        [EnumMember(Value = "pre_authorized")]
        PreAuthorized,
        Succeeded,
        Failed,
        Reversed,
        Canceled,
        Refunded,
        Dispute,
        [EnumMember(Value = "charged_back")]
        ChargedBack
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SourceUsage
    {
        [EnumMember(Value = "single_use")]
        SingleUse,
        [EnumMember(Value = "reusable")]

        Reusable,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SourceType
    {
        [EnumMember(Value = "wallet")]
        Wallet,
        [EnumMember(Value = "card")]
        Card,
        [EnumMember(Value = "card_and_wallet")]
        CardAndWallet,
        [EnumMember(Value = "token")]
        Token,
        [EnumMember(Value = "customer")]
        Customer,
        [EnumMember(Value = "three_d_segure")]
        ThreeDSegure,
        [EnumMember(Value = "debit_online")]
        DebitOnline,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SourceInstallmentPlanMode
    {
        /// <summary>
        /// SEM JUROS
        /// </summary>
        [EnumMember(Value = "interest_free")]
        InterestFree,
        /// <summary>
        /// COM JUROS
        /// </summary>
        [EnumMember(Value = "with_interest")]
        WithInterest
    }

    /// <summary>
    /// Moedas suportadas
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CurrencyType
    {
        /// <summary>
        /// moeda brasileira
        /// </summary>
        BRL,
        /// <summary>
        /// moeda americana
        /// </summary>
        USD,
        /// <summary>
        /// moeda europeia
        /// </summary>
        EUR
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebHookEvents
    {
        [EnumMember(Value = "transaction.canceled")]
        TransactionCanceled,
        [EnumMember(Value = "transaction.succeeded")]
        TransactionSucceeded,
        [EnumMember(Value = "transaction.failed")]
        TransactionFailed,
        [EnumMember(Value = "transaction.reversed")]
        TransactionReversed,
        [EnumMember(Value = "transaction.updated")]
        TransactionUpdated,
        [EnumMember(Value = "transaction.disputed")]
        TransactionDisputed,
        [EnumMember(Value = "transaction.disputed.succeeded")]
        TransactionDisputedSucceeded,
        [EnumMember(Value = "transaction.charged_back")]
        TransactionChargedBack,
        [EnumMember(Value = "transfer.failed")]
        TransferFailed,
        [EnumMember(Value = "transfer.succeeded")]
        TransferSucceeded,
        [EnumMember(Value = "transfer.confirmed")]
        TransferConfirmed,
        [EnumMember(Value = "transfer.delayed")]
        TransferDelayed,
        [EnumMember(Value = "transfer.created")]
        TransferCreated,
        [EnumMember(Value = "seller.updated")]
        SellerUpdated,
        [EnumMember(Value = "seller.activated")]
        SellerActivated,
        [EnumMember(Value = "seller.enabled")]
        SellerEnabled,
        [EnumMember(Value = "seller.deleted")]
        SellerDeleted,
        [EnumMember(Value = "subscription.created")]
        SubscriptionCreated,
        [EnumMember(Value = "subscription.updated")]
        SubscriptionUpdated,
        [EnumMember(Value = "subscription.canceled")]
        SubscriptionCanceled,
        [EnumMember(Value = "receivable.created")]
        ReceivableCreated,
        [EnumMember(Value = "receivable.paid")]
        ReceivablePaid,
        [EnumMember(Value = "receivable.refunded")]
        receivableRefunded,
        [EnumMember(Value = "receivable.deleted")]
        ReceivableDeleted,
        [EnumMember(Value = "receivable.canceled")]
        ReceivableCanceled,
        [EnumMember(Value = "bank_account.created")]
        BankAccountCreated,
        [EnumMember(Value = "bank_account.associated")]
        BankAccountAssociated,
        [EnumMember(Value = "bank_account.deactivated")]
        BankAccountDeactivated,
        [EnumMember(Value = "bank_account.deleted")]
        BankAccountDeleted,
        [EnumMember(Value = "card.created")]
        CardCreated,
        [EnumMember(Value = "card.updated")]
        CardUpdated,
        [EnumMember(Value = "card.deactivated")]
        CardDeactivated,
        [EnumMember(Value = "card.deleted")]
        CardDeleted
    }

}