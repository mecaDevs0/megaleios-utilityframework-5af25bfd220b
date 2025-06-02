namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripePaymentRequest
    {
        /// <summary>
        /// Valor da transação (Valor mínimo: 1.00)
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// MOEDA (Padrão: brl)
        /// </summary>
        public string Currency { get; set; } = "brl";
        /// <summary>
        /// //Identificador do cliente
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// Dados do cliente
        /// </summary>
        public StripeCustomerRequest Customer { get; set; }
        /// <summary>
        /// Descrição da transação
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Conta recebedora dos fundos
        /// </summary>
        public string ReceivingAccount { get; set; }
    }
}