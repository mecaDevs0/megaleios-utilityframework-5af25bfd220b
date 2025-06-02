
using System;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class PaymentInfoResponseViewModel
    {

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("amount_cents")]
        public int? AmountCents { get; set; }

        [JsonProperty("fine_cents")]
        public int? FineCents { get; set; }

        [JsonProperty("interest_cents")]
        public int? InterestCents { get; set; }

        [JsonProperty("discount_cents")]
        public int? DiscountCents { get; set; }

        [JsonProperty("total_amount_cents")]
        public int? TotalAmountCents { get; set; }

        [JsonProperty("recipient_name")]
        public string RecipientName { get; set; }

        [JsonProperty("recipient_cnpj_cpf")]
        public string RecipientCnpjCpf { get; set; }

        [JsonProperty("payer_name")]
        public string PayerName { get; set; }

        [JsonProperty("payer_cnpj_cpf")]
        public string PayerCnpjCpf { get; set; }

        [JsonProperty("allow_amount_change")]
        public bool? AllowAmountChange { get; set; }

        [JsonProperty("allow_partial_payment")]
        public bool? AllowPartialPayment { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("maximum_payment_date")]
        public DateTime? MaximumPaymentDate { get; set; }

        [JsonProperty("details")]
        public object Details { get; set; }

        [JsonProperty("emitter")]
        public string Emitter { get; set; }

        [JsonProperty("payee_cnpj_cpf")]
        public object PayeeCnpjCpf { get; set; }
    }
}