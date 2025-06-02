using System.Threading.Tasks;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3.Interfaces
{
    public interface IStripeTransferService
    {
        /// <summary>
        /// Cria uma transferência usando os dados fornecidos no StripeTransferRequest.
        /// </summary>
        /// <param name="request">Objeto contendo os dados necessários para criar a transferência.</param>
        /// <returns>Resposta base contendo os detalhes da transferência criada.</returns>
        Task<StripeBaseResponse<Transfer>> CreateTransferAsync(StripeTransferRequest request);

        /// <summary>
        /// Cria uma transferência usando as opções completas fornecidas no TransferCreateOptions.
        /// </summary>
        /// <param name="options">Opções detalhadas para criar a transferência.</param>
        /// <returns>Resposta base contendo os detalhes da transferência criada.</returns>
        Task<StripeBaseResponse<Transfer>> CreateTransferFullDataAsync(TransferCreateOptions options);


    }
}