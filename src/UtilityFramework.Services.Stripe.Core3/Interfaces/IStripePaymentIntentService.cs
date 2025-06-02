

using System.Threading.Tasks;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3.Interfaces
{
    public interface IStripePaymentIntentService
    {
        /// <summary>
        /// DETALHES DA TRANSAÇÃO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<PaymentIntent>> GetByIdAsync(string id);
        /// <summary>
        /// DETALHES COMPLETOS DA TRANSAÇÃO (PaymentIntent | Charge | BalanceTransaction)
        /// </summary>
        /// <param name="paymentIntentId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<StripePaymentIntentDetails>> GetPaymentIntentDetailsAsync(string paymentIntentId);
        /// <summary>
        /// CRIAR UM PAGAMENTO ANONIMO CARTÃO DE CRÉDITO
        /// </summary>
        /// <param name="request"></param>
        /// <param name="paymentMethodId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<PaymentIntent>> CreateAnonymousCreditCardPaymentAsync(StripeAnonymousPaymentRequest request);
        /// <summary>
        /// CRIAR UM PAGAMENTO CARTÃO DE CRÉDITO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<PaymentIntent>> CreateCreditCardPaymentAsync(StripeTransactionCreditCardRequest request);
        /// <summary>
        /// CRIAR UM PAGAMENTO PIX
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<PaymentIntent>> PixPaymentAsync(StripePixPaymentRequest request);
        /// <summary>
        /// CRIAR UM PAGAMENTO POR BOLETO
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<StripeBaseResponse> BankSlipPaymentAsync(StripeBankSlipPaymentRequest request);
        /// <summary>
        /// ESTORNAR UM PAGAMENTO
        /// </summary>
        /// <param name="paymentIntentId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<StripeBaseResponse> RefundPaymentAsync(string paymentIntentId, long? amount = null);

        /// <summary>
        /// CAPTURAR TRANSAÇÃO PRE-CAPTURADA
        /// </summary>
        /// <param name="paymentIntentId"></param>
        /// <param name="amountToCapture"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<PaymentIntent>> CapturePaymentIntentAsync(string paymentIntentId, long? amountToCapture = null);
    }
}