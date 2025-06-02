using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAccountCompleteModel : IuguBaseErrors
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("can_receive?")]
        public bool CanReceive { get; set; }
        [JsonProperty("is_verified?")]
        public bool IsVerified { get; set; }
        [JsonProperty("last_verification_request_status")]
        public string LastVerificationRequestStatus { get; set; }
        [JsonProperty("last_verification_request_data")]
        public IuguAccountDataVerificationModel LastVerificationData { get; set; }
        [JsonProperty("last_verification_request_feedback")]
        public object LastVerificationRequestFeedback { get; set; }
        [JsonProperty("change_plan_type")]
        public int ChangePlanType { get; set; }
        [JsonProperty("subscriptions_trial_period")]
        public int SubscriptionsTrialPeriod { get; set; }
        [JsonProperty("disable_emails")]
        public bool DisableEmails { get; set; }
        [JsonProperty("last_withdraw")]
        public object LastWithdraw { get; set; }
        [JsonProperty("total_subscriptions")]
        public int TotalSubscriptions { get; set; }
        [JsonProperty("reply_to")]
        public object ReplyTo { get; set; }
        [JsonProperty("webapp_on_test_mode")]
        public bool WebappOnTestMode { get; set; }
        [JsonProperty("marketplace")]
        public bool Marketplace { get; set; }
        [JsonProperty("auto_withdraw")]
        public bool AutoWithdraw { get; set; }
        [JsonProperty("balance")]
        public string Balance { get; set; }
        [JsonProperty("balance_available_for_withdraw")]
        public string BalanceAvailableForWithdraw { get; set; }
        [JsonProperty("balance_in_protest")]
        public string BalanceInProtest { get; set; }
        [JsonProperty("protected_balance")]
        public string ProtectedBalance { get; set; }
        [JsonProperty("payable_balance")]
        public string PayableBalance { get; set; }
        [JsonProperty("receivable_balance")]
        public string ReceivableBalance { get; set; }
        [JsonProperty("commission_balance")]
        public string CommissionBalance { get; set; }
        [JsonProperty("volume_last_month")]
        public string VolumeLastMonth { get; set; }
        [JsonProperty("volume_this_month")]
        public string VolumeThisMonth { get; set; }
        [JsonProperty("taxes_paid_last_month")]
        public string TaxesPaidLastMonth { get; set; }
        [JsonProperty("taxes_paid_this_month")]
        public string TaxesPaidThisMonth { get; set; }
        [JsonProperty("custom_logo_url")]
        public object CustomLogoUrl { get; set; }
        [JsonProperty("custom_logo_small_url")]
        public object CustomLogoSmallUrl { get; set; }

        [JsonProperty("informations")]
        public List<IuguInformationModel> Informations { get; set; }

        [JsonProperty("subscriptions_billing_days")]
        public int SubscriptionsBillingDays { get; set; }

        [JsonProperty("default_return_url")]
        public object DefaultReturnUrl { get; set; }

        [JsonProperty("credit_card_verified")]
        public bool? CreditCardVerified { get; set; }

        [JsonProperty("fines")]
        public object Fines { get; set; }

        [JsonProperty("late_payment_fine")]
        public object LatePaymentFine { get; set; }

        [JsonProperty("per_day_interest")]
        public object PerDayInterest { get; set; }

        [JsonProperty("old_advancement")]
        public bool? OldAdvancement { get; set; }

        [JsonProperty("early_payment_discount")]
        public object EarlyPaymentDiscount { get; set; }

        [JsonProperty("early_payment_discount_days")]
        public object EarlyPaymentDiscountDays { get; set; }

        [JsonProperty("early_payment_discount_percent")]
        public object EarlyPaymentDiscountPercent { get; set; }

        [JsonProperty("disabled_withdraw")]
        public bool? DisabledWithdraw { get; set; }

        [JsonProperty("payment_email_notification")]
        public bool? PaymentEmailNotification { get; set; }

        [JsonProperty("payment_email_notification_receiver")]
        public object PaymentEmailNotificationReceiver { get; set; }

        [JsonProperty("auto_advance")]
        public bool? AutoAdvance { get; set; }

        [JsonProperty("auto_advance_type")]
        public string AutoAdvanceType { get; set; }

        [JsonProperty("auto_advance_option")]
        public int? AutoAdvanceOption { get; set; }

        [JsonProperty("total_active_subscriptions")]
        public int? TotalActiveSubscriptions { get; set; }

        [JsonProperty("has_bank_address?")]
        public bool? HasBankAddress { get; set; }

        [JsonProperty("permissions")]
        public object Permissions { get; set; }

        [JsonProperty("early_payment_discounts")]
        public List<object> EarlyPaymentDiscounts { get; set; }

        [JsonProperty("commissions")]
        public object Commissions { get; set; }

        [JsonProperty("splits")]
        public List<object> Splits { get; set; }

        [JsonProperty("contact_data")]
        public ContactDataResponseModel ContactData { get; set; }

        [JsonProperty("configuration")]
        public ConfigurationResponseModel Configuration { get; set; }

        [JsonProperty("bank_accounts")]
        public List<BankAccountResponseModel> BankAccounts { get; set; }
    }
}