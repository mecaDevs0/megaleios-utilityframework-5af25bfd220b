using System;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeBankSlipPaymentRequest : StripePaymentRequest
    {
        public DateTime? DueDate { get; set; }
    }
}