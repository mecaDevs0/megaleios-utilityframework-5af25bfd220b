using System.Threading.Tasks;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopBankAccountService
    {
        /// <summary>
        /// OBTER DADOS DE UM TOKEN DE CONTA BANCÁRIA
        /// </summary>
        /// <param name="token_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GetTokenBank(string token_id, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS DE UM TOKEN DE CONTA BANCÁRIA
        /// </summary>
        /// <param name="token_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GetTokenBankAsync(string token_id, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REGISTRAR TOKEN DE CONTA BANCÁRIA
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> RegisterTokenBank(RegisterTokenBankAccountViewModel model, string marketPlaceId = null);
        /// <summary>
        /// REGISTRAR TOKEN DE CONTA BANCÁRIA
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> RegisterTokenBankAsync(RegisterTokenBankAccountViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REMOVER CONTA BANCÁRIA
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> DeleteBank(string bankId, string marketPlaceId = null);
        /// <summary>
        /// REMOVER CONTA BANCÁRIA
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> DeleteBankAsync(string bankId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// ASSOCIAR CONTA BANCARIA A UM VENDEDOR OU COMPRADOR
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> AssociateTokenBankWithCustomer(AssociateViewModel model, string marketPlaceId = null);
        /// <summary>
        /// ASSOCIAR CONTA BANCARIA A UM VENDEDOR OU COMPRADOR
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> AssociateTokenBankWithCustomerAsync(AssociateViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR DADOS BANCARIOS POR SELLER
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GetBanksBySeller(string sellerId, string marketPlaceId = null);
        /// <summary>
        /// LISTAR DADOS BANCARIOS POR SELLER
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GetBanksBySellerAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR DADOS BANCARIOS POR SELLER
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GetBanksByMarketPlace(string marketPlaceId = null);
        /// <summary>
        /// LISTAR DADOS BANCARIOS POR SELLER
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GetBanksByMarketPlaceAsync(string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// OBTER DADOS BANCARIOS
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GetBankInformation(string bankId, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS BANCARIOS
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GetBankInformationAsync(string bankId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// SOLICITAR TRANSFERENCIA DE VALORES MANUALMENTE PARA CONTA DO VENDEDOR
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> RequestTransfer(RequestTransferViewModel model,string bankId, string marketPlaceId = null);
                /// <summary>
        /// SOLICITAR TRANSFERENCIA DE VALORES MANUALMENTE PARA CONTA DO VENDEDOR
        /// </summary>
        /// <param name="bankId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> RequestTransferAsync(RequestTransferViewModel model,string bankId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// SOLICITAR TRANSFERENCIAS DE VALORES ENTRE 2 SUBCONTAS (SELLERS)
        /// </summary>
        /// <param name="sellerSender"></param>
        /// <param name="sellerReceive"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> RequestInternalTransfer(RequestTransferViewModel model, string sellerSender, string sellerReceive, string marketPlaceId = null);
                /// <summary>
        /// SOLICITAR TRANSFERENCIAS DE VALORES ENTRE 2 SUBCONTAS (SELLERS)
        /// </summary>
        /// <param name="sellerSender"></param>
        /// <param name="sellerReceive"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> RequestInternalTransferAsync(RequestTransferViewModel model, string sellerSender, string sellerReceive, string marketPlaceId = null, bool configureAwait = false);
    }
}