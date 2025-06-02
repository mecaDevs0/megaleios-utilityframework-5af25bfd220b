using System;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeTransactionPix : StripePaymentRequest
    {
        /// <summary>
        /// Data de vencimento da transação Pix.
        /// </summary>
        public DateTime? OverDue { get; set; }
    }
}
