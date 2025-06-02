using System.Threading.Tasks;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3.Interfaces
{
    public interface IStripePaymentMethodService
    {
        /// <summary>
        /// Lista todos os cartões de crédito associados a um método de pagamento no Stripe.
        /// </summary>
        /// <param name="customerId">O identificador do cliente.</param>
        /// <returns>Uma resposta contendo uma lista de objetos de métodos de pagamento.</returns>
        Task<StripeBaseResponse<StripeList<PaymentMethod>>> ListCreditCardAsync(string customerId);

        /// <summary>
        /// Busca um cartão de crédito específico pelo seu identificador no Stripe.
        /// </summary>
        /// <param name="paymentMethodId">O identificador do método de pagamento.</param>
        /// <returns>Uma resposta contendo o método de pagamento encontrado.</returns>
        Task<StripeBaseResponse<PaymentMethod>> FindCreditCardByIdAsync(string paymentMethodId);

        /// <summary>
        /// Vincula um cartão de crédito a um cliente no Stripe.
        /// </summary>
        /// <param name="customerId">O identificador do cliente.</param>
        /// <param name="paymentMethodId">O identificador do método de pagamento.</param>
        /// <returns>Uma resposta contendo o método de pagamento vinculado.</returns>
        Task<StripeBaseResponse<PaymentMethod>> LinkCreditCardToCustomerAsync(string customerId, string paymentMethodId);

        /// <summary>
        /// Exclui um cartão de crédito associado a um método de pagamento no Stripe.
        /// </summary>
        /// <param name="paymentMethodId">O identificador do método de pagamento.</param>
        /// <returns>Uma resposta indicando o sucesso ou falha da operação.</returns>
        Task<StripeBaseResponse> DeleteCreditCardAsync(string paymentMethodId);
    }
}