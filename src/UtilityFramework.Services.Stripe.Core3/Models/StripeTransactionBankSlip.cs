using System;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeTransactionBankSlip : StripePaymentRequest
    {
        /// <summary>
        /// Data de vencimento do boleto bancário.
        /// </summary>
        public DateTime? OverDue { get; set; }
    }
}
