using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopSellerService
    {
        /// <summary>
        /// LISTAR DADOS DO VENDEDOR
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        SellerViewModel GetSellerById(string sellerId, string marketPlaceId = null);
        /// <summary>
        /// LISTAR DADOS DO VENDEDOR
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<SellerViewModel> GetSellerByIdAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR DADOS DO VENDEDOR
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        SellerViewModel GetSellerByCpfOrCnpj(FilterSellerViewModel filter, string marketPlaceId = null);
        /// <summary>
        /// LISTAR DADOS DO VENDEDOR
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<SellerViewModel> GetSellerByCpfOrCnpjAsync(FilterSellerViewModel filter, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR VENDEDORES
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<SellerViewModel> ListSellers<SellerViewModel>(ZoopPaginationViewModel pagination = null, string marketPlaceId = null);
        /// <summary>
        /// LISTAR VENDEDORES
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<SellerViewModel>> ListSellersAsync<SellerViewModel>(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REGISTRAR VENDEDOR TIPO EMPRESA
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        SellerViewModel RegisterSellerBusiness(SellerViewModel model, string marketPlaceId = null);
        /// <summary>
        /// REGISTRAR VENDEDOR TIPO EMPRESA
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<SellerViewModel> RegisterSellerBusinessAsync(SellerViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REGISTRAR VENDEDOR TIPO INDIVIDUAL
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        SellerViewModel RegisterSellerIndividual(SellerViewModel model, string marketPlaceId = null);
        /// <summary>
        /// REGISTRAR VENDEDOR TIPO INDIVIDUAL
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<SellerViewModel> RegisterSellerIndividualAsync(SellerViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REMOVER VENDEDOR
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> DeleteSeller(string sellerId, string marketPlaceId = null);
        /// <summary>
        ///
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> DeleteSellerAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR CATEGORIA DE VENDEDORES
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        BaseResponseViewModel<ItemMerchantCategoryCodeViewModel> ListMCC(ZoopPaginationViewModel pagination = null);
        /// <summary>
        /// LISTAR CATEGORIA DE VENDEDORES
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>> ListMCCAsync(ZoopPaginationViewModel pagination = null, bool configureAwait = false);
        /// <summary>
        /// REGISTRAR DOCUMENTOS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> RegisterDocument(IFormFile file, RegisterDocumentViewModel model, string sellerId, string marketPlaceId = null);

        /// <summary>
        /// REGISTRAR DOCUMENTOS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<ResponseUploadViewModel> RegisterDocumentAsync(IFormFile file, RegisterDocumentViewModel model, string sellerId, string marketPlaceId = null, bool configureAwait = false);

        /// <summary>
        /// ALTERAR SE IRA FAZER TRANSAFERENCIA AUTOMATICA OU MANUALMENTE PARA CONTA DO VENDEDOR
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> ChangeAutomaticTransfer(ChangeAutomaticTransferViewModel model, string sellerId, string marketPlaceId = null);
        /// <summary>
        /// ALTERAR SE IRA FAZER TRANSAFERENCIA AUTOMATICA OU MANUALMENTE PARA CONTA DO VENDEDOR
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sellerId"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> ChangeAutomaticTransferAsync(ChangeAutomaticTransferViewModel model, string sellerId, string marketPlaceId = null, bool configureAwait = false);
    }
}