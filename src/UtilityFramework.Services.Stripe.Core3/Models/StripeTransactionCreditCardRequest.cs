using Stripe;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeTransactionCreditCardRequest : StripePaymentRequest
    {
        /// <summary>
        /// Capturar na hora (Padrão: true)
        /// </summary>
        public bool Capture { get; set; } = true;
        /// <summary>
        /// Identificador do cartão de crédito
        /// </summary>
        public string CreditCardId { get; set; }
        /// <summary>
        /// Número de parcelas
        /// </summary>
        public int Installments { get; set; }
    }
}