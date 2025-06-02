using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class ConfigurationResponseModel
    {

        [JsonProperty("auto_withdraw")]
        public bool? AutoWithdraw { get; set; }

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

        [JsonProperty("commission_percent")]
        public int? CommissionPercent { get; set; }

        [JsonProperty("owner_emails_to_notify")]
        public object OwnerEmailsToNotify { get; set; }

        [JsonProperty("fines")]
        public object Fines { get; set; }

        [JsonProperty("late_payment_fine")]
        public object LatePaymentFine { get; set; }

        [JsonProperty("per_day_interest")]
        public object PerDayInterest { get; set; }

        [JsonProperty("bank_slip")]
        public BankSlipResponseModel BankSlip { get; set; }

        [JsonProperty("credit_card")]
        public CreditCardResponseModel CreditCard { get; set; }

        [JsonProperty("pix")]
        public PixRespondeModel Pix { get; set; }

        [JsonProperty("early_payment_discount")]
        public object EarlyPaymentDiscount { get; set; }

        [JsonProperty("early_payment_discount_days")]
        public object EarlyPaymentDiscountDays { get; set; }

        [JsonProperty("early_payment_discount_percent")]
        public object EarlyPaymentDiscountPercent { get; set; }
    }



}