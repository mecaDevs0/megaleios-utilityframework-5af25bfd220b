using System.Threading.Tasks;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopCreditCardService

    {
        /// <summary>
        /// ASSOCIAR CARTÃO AO COMPRADOR
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<CardViewModel> AssociateCreditCardAsync(AssociateViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// GERAR TOKEN DE UM CARTÃO
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GenerateTokenCardAsync(RegisterCreditCardViewModel model, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// OBTER DADOS DE UM TOKEN 
        /// </summary>
        /// <param name="token_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> GetTokenCardAsync(string token_id, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// OBTER DADOS DE UM CARTÃO ASSOCIADO A UM COMPRADOR
        /// </summary>
        /// <param name="credit_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>

        Task<CardViewModel> GetCardAsync(string credit_id, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// REMOVER CARTÃO DE CREDITO
        /// </summary>
        /// <param name="creditCard_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        Task<BaseResponseViewModel<object>> DeleteCardAsync(string creditCard_id, string marketPlaceId = null, bool configureAwait = false);
        /// <summary>
        /// ASSOCIAR CARTÃO A UM COMPRADOR 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        CardViewModel AssociateCreditCard(AssociateViewModel model, string marketPlaceId = null);
        /// <summary>
        /// REMOVER CARTÃO
        /// </summary>
        /// <param name="creditCard_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> DeleteCard(string creditCard_id, string marketPlaceId = null);
        /// <summary>
        /// GERAR TOKEN DE CARTÃO DE CREDITO
        /// </summary>
        /// <param name="model"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GenerateTokenCard(RegisterCreditCardViewModel model, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS DE UM TOKEN
        /// </summary>
        /// <param name="token_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        BaseResponseViewModel<object> GetTokenCard(string token_id, string marketPlaceId = null);
        /// <summary>
        /// OBTER DADOS DO CARTÃO DE UM COMPRADOR 
        /// </summary>
        /// <param name="credit_id"></param>
        /// <param name="marketPlaceId"></param>
        /// <returns></returns>
        CardViewModel GetCard(string credit_id, string marketPlaceId = null);
        BaseResponseViewModel<CardViewModel> ListCard(ZoopPaginationViewModel pagination = null, string marketPlaceId = null);
        Task<BaseResponseViewModel<CardViewModel>> ListCardAsync(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false);



    }
}