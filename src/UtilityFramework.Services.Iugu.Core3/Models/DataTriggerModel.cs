using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class DataTriggerModel
    {
        [JsonProperty("account_id")]
        [ModelBinder(Name = "account_id")]
        public string AccountId { get; set; }
        [JsonProperty("account_number_last_digits")]
        [ModelBinder(Name = "account_number_last_digits")]
        public string AccountNumberLastDigits { get; set; }
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("advancement_request_date")]
        [ModelBinder(Name = "advancement_request_date")]
        public string AdvancementRequestDate { get; set; }
        [JsonProperty("agreement_effect")]
        [ModelBinder(Name = "agreement_effect")]
        public string AgreementEffect { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("amount_cents")]
        [ModelBinder(Name = "amount_cents")]
        public string AmountCents { get; set; }
        [JsonProperty("async_charged")]
        [ModelBinder(Name = "async_charged")]
        public string AsyncCharged { get; set; }
        [JsonProperty("available_amount_cents")]
        [ModelBinder(Name = "available_amount_cents")]
        public string AvailableAmountCents { get; set; }
        [JsonProperty("average_days")]
        [ModelBinder(Name = "average_days")]
        public string AverageDays { get; set; }
        [JsonProperty("charge_limit_cents")]
        [ModelBinder(Name = "charge_limit_cents")]
        public string ChargeLimitCents { get; set; }
        [JsonProperty("client_share")]
        [ModelBinder(Name = "client_share")]
        public string ClientShare { get; set; }
        [JsonProperty("credit_card")]
        [ModelBinder(Name = "credit_card")]
        public CreditCardWebHook CreditCard { get; set; }
        [JsonProperty("customer_email")]
        [ModelBinder(Name = "customer_email")]
        public string CustomerEmail { get; set; }
        [JsonProperty("customer_id")]
        [ModelBinder(Name = "customer_id")]
        public string CustomerId { get; set; }
        [JsonProperty("customer_name")]
        [ModelBinder(Name = "customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty("customer_payment_method_id")]
        [ModelBinder(Name = "customer_payment_method_id")]
        public string CustomerPaymentMethodId { get; set; }
        [JsonProperty("default_payment_method_id")]
        [ModelBinder(Name = "default_payment_method_id")]
        public string DefaultPaymentMethodId { get; set; }
        [JsonProperty("deposit_id")]
        [ModelBinder(Name = "deposit_id")]
        public string DepositId { get; set; }
        [JsonProperty("deposit_type")]
        [ModelBinder(Name = "deposit_type")]
        public string DepositType { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("discount")]
        public string Discount { get; set; }
        [JsonProperty("early_payment_discount")]
        [ModelBinder(Name = "early_payment_discount")]
        public string EarlyPaymentDiscount { get; set; }
        [JsonProperty("expires_at")]
        [ModelBinder(Name = "expires_at")]
        public string ExpiresAt { get; set; }
        [JsonProperty("external_reference")]
        [ModelBinder(Name = "external_reference")]
        public string ExternalReference { get; set; }
        [JsonProperty("feedback")]
        public string Feedback { get; set; }
        [JsonProperty("fines")]
        public string Fines { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("installment")]
        public string Installment { get; set; }
        [JsonProperty("lr")]
        public string LR { get; set; }
        [JsonProperty("net_value")]
        [ModelBinder(Name = "net_value")]
        public string NetValue { get; set; }
        [JsonProperty("number_of_installments")]
        [ModelBinder(Name = "number_of_installments")]
        public string NumberOfInstallments { get; set; }
        [JsonProperty("occurrence_date")]
        [ModelBinder(Name = "occurrence_date")]
        public string OccurrenceDate { get; set; }
        [JsonProperty("order_id")]
        [ModelBinder(Name = "order_id")]
        public string OrderId { get; set; }
        [JsonProperty("paid_at")]
        [ModelBinder(Name = "paid_at")]
        public string PaidAt { get; set; }
        [JsonProperty("paid_cents")]
        [ModelBinder(Name = "paid_cents")]
        public string PaidCents { get; set; }
        [JsonProperty("payer_cpf_cnpj")]
        [ModelBinder(Name = "payer_cpf_cnpj")]
        public string PayerCpfCnpj { get; set; }
        [JsonProperty("payer_name")]
        [ModelBinder(Name = "payer_name")]
        public string PayerName { get; set; }
        [JsonProperty("payment_method")]
        [ModelBinder(Name = "payment_method")]
        public string PaymentMethod { get; set; }
        [JsonProperty("pix_end_to_end_id")]
        [ModelBinder(Name = "pix_end_to_end_id")]
        public string PixEndToEndId { get; set; }
        [JsonProperty("plan_identifier")]
        [ModelBinder(Name = "plan_identifier")]
        public string PlanIdentifier { get; set; }
        [JsonProperty("reached_amount_cents")]
        [ModelBinder(Name = "reached_amount_cents")]
        public string ReachedAmountCents { get; set; }
        [JsonProperty("receiver_bank_account")]
        [ModelBinder(Name = "receiver_bank_account")]
        public string ReceiverBankAccount { get; set; }
        [JsonProperty("receiver_bank_account_digit")]
        [ModelBinder(Name = "receiver_bank_account_digit")]
        public string ReceiverBankAccountDigit { get; set; }
        [JsonProperty("receiver_bank_branch")]
        [ModelBinder(Name = "receiver_bank_branch")]
        public string ReceiverBankBranch { get; set; }
        [JsonProperty("receiver_bank_name")]
        [ModelBinder(Name = "receiver_bank_name")]
        public string ReceiverBankName { get; set; }
        [JsonProperty("receiver_compe")]
        [ModelBinder(Name = "receiver_compe")]
        public string ReceiverCompe { get; set; }
        [JsonProperty("receiver_cpf_cnpj")]
        [ModelBinder(Name = "receiver_cpf_cnpj")]
        public string ReceiverCpfCnpj { get; set; }
        [JsonProperty("receiver_ispb")]
        [ModelBinder(Name = "receiver_ispb")]
        public string ReceiverIspb { get; set; }
        [JsonProperty("receiver_name")]
        [ModelBinder(Name = "receiver_name")]
        public string ReceiverName { get; set; }
        [JsonProperty("recipient_account_id")]
        [ModelBinder(Name = "recipient_account_id")]
        public string RecipientAccountId { get; set; }
        [JsonProperty("refunded_amount_cents")]
        [ModelBinder(Name = "refunded_amount_cents")]
        public string RefundedAmountCents { get; set; }
        [JsonProperty("requested_amount_cents")]
        [ModelBinder(Name = "requested_amount_cents")]
        public string RequestedAmountCents { get; set; }
        [JsonProperty("sender_bank_account")]
        [ModelBinder(Name = "sender_bank_account")]
        public string SenderBankAccount { get; set; }
        [JsonProperty("sender_bank_branch")]
        [ModelBinder(Name = "sender_bank_branch")]
        public string SenderBankBranch { get; set; }
        [JsonProperty("sender_bank_name")]
        [ModelBinder(Name = "sender_bank_name")]
        public string SenderBankName { get; set; }
        [JsonProperty("sender_compe")]
        [ModelBinder(Name = "sender_compe")]
        public string SenderCompe { get; set; }
        [JsonProperty("sender_cpf_cnpj")]
        [ModelBinder(Name = "sender_cpf_cnpj")]
        public string SenderCpfCnpj { get; set; }
        [JsonProperty("sender_ispb")]
        [ModelBinder(Name = "sender_ispb")]
        public string SenderIspb { get; set; }
        [JsonProperty("sender_name")]
        [ModelBinder(Name = "sender_name")]
        public string SenderName { get; set; }
        [JsonProperty("simulation_amount_cents")]
        [ModelBinder(Name = "simulation_amount_cents")]
        public string SimulationAmountCents { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("statement_identifier")]
        [ModelBinder(Name = "statement_identifier")]
        public string StatementIdentifier { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("subscription_id")]
        [ModelBinder(Name = "subscription_id")]
        public string SubscriptionId { get; set; }
        [JsonProperty("taxes")]
        public string Taxes { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("total_advance_amount_cents")]
        [ModelBinder(Name = "total_advance_amount_cents")]
        public string TotalAdvanceAmountCents { get; set; }
        [JsonProperty("total_advance_fee_cents")]
        [ModelBinder(Name = "total_advance_fee_cents")]
        public string TotalAdvanceFeeCents { get; set; }
        [JsonProperty("total_refunded_amount_cents")]
        [ModelBinder(Name = "total_refunded_amount_cents")]
        public string TotalRefundedAmountCents { get; set; }
        [JsonProperty("transaction_ids")]
        [ModelBinder(Name = "transaction_ids")]
        public string TransactionIds { get; set; }
        [JsonProperty("transfered_at")]
        [ModelBinder(Name = "transfered_at")]
        public string TransferedAt { get; set; }
        [JsonProperty("withdraw_request_id")]
        [ModelBinder(Name = "withdraw_request_id")]
        public string WithdrawRequestId { get; set; }
    }
}