using System.Threading.Tasks;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3.Interfaces
{
    public interface IStripeCustomerService
    {
        /// <summary>
        /// Buscar um cliente pelo Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Customer>> FindCustomerByIdAsync(string customerId);
        /// <summary>
        /// Buscar cliente pelo documento (CPF/CNPJ)
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Customer>> FindCustomerByDocumentAsync(string document);
        /// <summary>
        /// Buscar cliente pelo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Customer>> FindCustomerByEmailAsync(string email);
        /// <summary>
        /// Cadastrar ou buscar um cliente pelo documento (CPF/CNPJ/Email)
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Customer>> CreateOrGetCustomerAsync(StripeCustomerRequest customerRequest);
        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse> DeleteCustomerAsync(string customerId);
        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Customer>> UpdateCustomerAsync(StripeCustomerRequest customerRequest);
    }
}