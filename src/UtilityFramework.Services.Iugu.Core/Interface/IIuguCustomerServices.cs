using System.Threading.Tasks;
using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Interface
{
    /// <summary>
    /// CLIENT IUGU
    /// </summary>
    public interface IIuguCustomerServices
    {
        /// <summary>
        /// REGISTRAR CLIENTE
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<IuguCustomerModel> SaveClientAsync(IuguCustomerModel customer);
        /// <summary>
        /// ATUALIZAR INFORMAÇÕES DO CLIENTE
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<IuguCustomerModel> UpdateClientAsync(string clientId, IuguCustomerModel customer);
        /// <summary>
        /// DEFINIR CARTÃO COMO PADRÃO
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="cardId"></param>
        /// <param name="cpfCnpj"></param>
        /// <returns></returns>
        Task<IuguCustomerModel> SetDefaultCartaoAsync(string clientId, string cardId, string cpfCnpj);
        /// <summary>
        /// DEFINIR CARTÃO COMO PADRÃO
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Task<IuguCustomerModel> SetDefaultCartaoAsync(string clientId, string cardId);
        /// <summary>
        /// REMOVER CLIENTE
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteClientAsync(string id);
    }
}