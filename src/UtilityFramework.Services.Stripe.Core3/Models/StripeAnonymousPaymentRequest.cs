namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeAnonymousPaymentRequest : StripePaymentRequest
    {
        /// <summary>
        /// Quantidade de parcelas
        /// </summary>
        public int Installments { get; set; } = 1;
        /// <summary>
        /// Dados do cartão de crédito
        /// </summary>
        public StripeCreditCardRequest CreditCard { get; set; }
    }
}