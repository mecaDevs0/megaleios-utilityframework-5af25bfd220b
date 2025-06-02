using System.Threading.Tasks;
using UtilityFramework.Services.Iugu.Core.Models;
using UtilityFramework.Services.Iugu.Core.Response;

namespace UtilityFramework.Services.Iugu.Core.Interface
{
    public interface IIuguService
    {
        /// <summary>
        /// ENVIAR VERIFICAÇÃO DE DADOS BANCÁRIOS 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userApiKey"></param>
        /// <param name="accountKey"></param>
        /// <param name="useGoogleVerificationAddress"></param>
        /// <param name="googleKey"></param>
        /// <returns></returns>
        Task<IuguVerifyAccountModel> SendRequestVerificationAsync(DataBankViewModel model, string userApiKey, string accountKey, bool useGoogleVerificationAddress = false, string googleKey = null);
        /// <summary>
        /// ATUALIZAR DADOS BANCÁRIOS 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="liveKey"></param>
        /// <returns></returns>
        Task<SimpleResponseMessage> UpdateDataBankAsync(DataBankViewModel model, string liveKey);

    }
}