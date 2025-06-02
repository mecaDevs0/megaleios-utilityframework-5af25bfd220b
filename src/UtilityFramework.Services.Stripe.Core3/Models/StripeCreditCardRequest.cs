namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeCreditCardRequest : StripeCustomerRequest
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Brand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
        public string Country { get; set; } = "BR";
        public string Currency { get; set; } = "BRL";
    }
}