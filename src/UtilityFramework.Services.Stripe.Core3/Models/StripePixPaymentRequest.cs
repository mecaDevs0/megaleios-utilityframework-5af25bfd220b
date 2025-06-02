using System;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripePixPaymentRequest : StripePaymentRequest
    {
        public DateTime? DueDate { get; set; }
    }
}