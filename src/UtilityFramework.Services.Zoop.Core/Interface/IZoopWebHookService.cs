using System.Threading.Tasks;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopWebHookService

    {
        /// <summary>
        /// REGISTRAR WEBHOOK
        /// </summary>
        /// <param name="model"></param>
        /// <param name="markerPlaceId"></param>
        /// <returns></returns>
        ResponseWebHookViewModel RegisterWebHook(RegisterWebHookViewModel model, string markerPlaceId = null);
        /// <summary>
        /// REGISTRAR WEBHOOK
        /// </summary>
        /// <param name="model"></param>
        /// <param name="markerPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<ResponseWebHookViewModel> RegisterWebHookAsync(RegisterWebHookViewModel model, string markerPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// DETALHES DO WEBHOOK POR ID
        /// </summary>
        /// <param name="webhookId"></param>
        /// <param name="markerPlaceId"></param>
        /// <returns></returns>
        ResponseWebHookViewModel GetWebHookById(string webhookId, string markerPlaceId = null);
        /// <summary>
        /// DETALHES DO WEBHOOK POR ID
        /// </summary>
        /// <param name="webhookId"></param>
        /// <param name="markerPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<ResponseWebHookViewModel> GetWebHookByIdAsync(string webhookId, string markerPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REMOVER WEBHOOK 
        /// </summary>
        /// <param name="webhookId"></param>
        /// <param name="markerPlaceId"></param>
        /// <returns></returns>
        ResponseWebHookViewModel DeleteWebHook(string webhookId, string markerPlaceId = null);
        /// <summary>
        /// REMOVER WEBHOOK
        /// </summary>
        /// <param name="webhookId"></param>
        /// <param name="markerPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<ResponseWebHookViewModel> DeleteWebHookAsync(string webhookId, string markerPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// LISTAR WEBBOOK
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="markerPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<ResponseWebHookViewModel> ListWebHook(ZoopPaginationViewModel pagination = null, string markerPlaceId = null);
        /// <summary>
        /// LISTAR WEBBOOK
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="markerPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<ResponseWebHookViewModel>> ListWebHookAsync(RegisterWebHookViewModel pagination = null, string markerPlaceId = null, bool configureAwait = false);
    }
}