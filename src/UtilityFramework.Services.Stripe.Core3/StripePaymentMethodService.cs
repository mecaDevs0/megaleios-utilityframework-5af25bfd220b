using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class StripePaymentMethodService(IHostingEnvironment env) : StripeBaseService(env), IStripePaymentMethodService
    {
        public async Task<StripeBaseResponse<StripeList<PaymentMethod>>> ListCreditCardAsync(string customerId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentMethodService();
                return await service.ListAsync(new PaymentMethodListOptions { Customer = customerId, Type = "card" });
            });
        }
        public async Task<StripeBaseResponse<PaymentMethod>> FindCreditCardByIdAsync(string paymentMethodId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentMethodService();

                return await service.GetAsync(paymentMethodId);
            });
        }

        public async Task<StripeBaseResponse<PaymentMethod>> LinkCreditCardToCustomerAsync(string customerId, string paymentMethodId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentMethodService();
                return await service.AttachAsync(paymentMethodId, new PaymentMethodAttachOptions { Customer = customerId });
            });
        }

        public async Task<StripeBaseResponse> DeleteCreditCardAsync(string paymentMethodId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentMethodService();

                return await service.DetachAsync(paymentMethodId);
            });
        }
    }
}