using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Interface
{
    /// <summary>
    /// FORMAS DE PAGAMENTO
    /// </summary>
    public interface IIuguPaymentMethodService
    {
        /// <summary>
        /// REGISTRAR CARTÃO DE CRÉDITO
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IuguPaymentMethodToken> SaveCreditCardAsync(IuguPaymentMethodToken model);
        /// <summary>
        /// LISTAR CARTÕES DO CLIENTE
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<IEnumerable<IuguCreditCard>> ListarCredCardsAsync(string clientId);
        /// <summary>
        /// VINCULAR CARTÃO A UM CLIENTE
        /// </summary>
        /// <param name="model"></param>
        /// <param name="clienteId"></param>
        /// <returns></returns>
        Task<IuguCreditCard> LinkCreditCardClientAsync(IuguCustomerPaymentMethod model, string clienteId);
        /// <summary>
        /// BUSCAR CARTAO DE UM CLIENTE POR ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Task<IuguCreditCard> BuscarCredCardsAsync(string clientId, string cardId);
        /// <summary>
        /// REMOVER CARTÃO POR ID
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Task<IuguCreditCard> RemoverCredCardAsync(string clientId, string cardId);


    }
}