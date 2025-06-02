using System.Threading.Tasks;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopBuyerService
    {
        /// <summary>
        /// REGISTRA UM COMPRADOR 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BuyerViewModel> RegisterBuyerAsync(BuyerViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REGISTRAR UM COMPRADOR 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BuyerViewModel RegisterBuyer(BuyerViewModel model, string marketPlaceId = null);
        /// <summary>
        /// LISTAR COMPRADORES DE UM MARKET PLACE
        /// </summary>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<BuyerViewModel>> ListBuyerByMarketPlaceIdAsync(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR COMPRADORES DE UM MARKET PLACE
        /// </summary>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        BaseResponseViewModel<BuyerViewModel> ListBuyerByMarketPlaceId(ZoopPaginationViewModel pagination = null, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS DO COMPRADOR
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BuyerViewModel> GetBuyerByIdAsync(string buyerId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// OBTER DADOS DO COMPRADOR
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BuyerViewModel GetBuyerById(string buyerId, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS DO COMPRADOR POR CPF OU CNPJ
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BuyerViewModel> GetBuyerByCpfOrCnpjAsync(string cpfOrCnpj, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// OBTER DADOS DO COMPRADOR POR CPF OU CNPJ
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BuyerViewModel GetBuyerByCpfOrCnpj(string cpfOrCnpj, string marketPlaceId = null);
        /// <summary>
        /// DELETAR COMPRADOR
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> DeleteBuyer(string buyerId, string marketPlaceId = null);
        /// <summary>
        /// DELETAR COMPRADOR
        /// </summary>
        /// <param name="buyerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> DeleteBuyerAsync(string buyerId, string marketPlaceId = null, bool configureAwait = false);
        

    }
}