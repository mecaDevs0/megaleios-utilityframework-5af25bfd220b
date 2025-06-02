using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Application.Core3.ViewModels;
using UtilityFramework.Services.Iugu.Core3.Entity;
using UtilityFramework.Services.Iugu.Core3.Models;
using UtilityFramework.Services.Iugu.Core3.Request;
using UtilityFramework.Services.Iugu.Core3.Response;

namespace UtilityFramework.Services.Iugu.Core3.Interface
{
    /// <summary>
    /// MARKET PLACE
    /// </summary>
    public interface IIuguMarketPlaceServices
    {
        /// <summary>
        /// TRANSFERIR VALORES ENTRE SUBCONTAS
        /// </summary>
        /// <param name="apiTokenSubConta"></param>
        /// <param name="accoutId"></param>
        /// <param name="valorDecimal"></param>
        /// <param name="apiTokenMaster"></param>
        /// <param name="toWithdraw"></param>
        /// <returns></returns>
        Task<IuguTransferModel> RepasseValoresAsync(string apiTokenSubConta, string accoutId, decimal valorDecimal, string apiTokenMaster = null, bool toWithdraw = true);
        /// <summary>
        /// SOLICITAR SAQUE DE VALORES PARA CONTA FÍSICA (OBRIGATÓRIO ASSINAR REQUEST)
        /// </summary>
        /// <param name="accoutId"></param>
        /// <param name="valorSaque"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguWithdrawalModel> SolicitarSaqueAsync(string accoutId, decimal valorSaque, string apiToken = null);
        /// <summary>
        /// CRIAR SUBCONTA (MARKETPLACE)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IuguAccountCreateResponseModel> CriarSubContaAsync(IuguAccountRequestModel model);
        /// <summary>
        /// VERIFICAR SUBCONTA (DADOS BANCÁRIOS)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userApiToken"></param>
        /// <param name="accoutId"></param>
        /// <returns></returns>
        Task<IuguVerifyAccountModel> VerificarSubContaAsync(IuguAccountVerificationModel model, string userApiToken, string accoutId);
        /// <summary>
        /// ATUALIZAR DADOS BANCÁRIOS (DOMICILIO BANCÁRIO)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userApiToken"></param>
        /// <returns></returns>
        Task<SimpleResponseMessage> AtualizarDadosBancariosSubContaAsync(IuguUpdateDataBank model, string userApiToken);
        /// <summary>
        /// ENVIO DE VERIFICAÇÃO DE DADOS BANCÁRIOS
        /// </summary>
        /// <param name="userApiToken"></param>
        /// <returns></returns>
        Task<List<IuguBankVerificationResponse>> VerificarAtualizacaoDadosBancariosSubContaAsync(string userApiToken);
        /// <summary>
        /// OBTER INFORMAÇÕES DA SUBCONTA (MARKETPLACE)
        /// </summary>
        /// <param name="accoutId"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguAccountCompleteModel> GetInfoSubContaAsync(string accoutId, string apiToken = null);
        /// <summary>
        /// CONFIGURAR INFORMAÇÕES DA SUBCONTA
        /// </summary>
        /// <param name="configurationRequest"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguAccountCompleteModel> ConfigurarSubContaAsync(AccountConfigurationRequestMessage configurationRequest, string apiToken = null);
        /// <summary>
        /// OBTER TODAS SUBCONTAS (MARKETPLACE) CADASTRADAS
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguMarketPlaceResponse> GetAccountsAsync(IuguAccountFilterModel filter, string apiToken = null);
        /// <summary>
        /// ENVIAR VERIFICAÇÃO OU UPDATE DE DADOS BANCÁRIOS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newRegister"></param>
        /// <param name="liveKey"></param>
        /// <param name="userApiKey"></param>
        /// <param name="accountKey"></param>
        /// <param name="lastVerification"></param>
        /// <param name="marketplaceName"></param>
        /// <param name="useGoogleVerificationAddress"></param>
        /// <param name="googleKey"></param>
        /// <param name="checkExists">USADO PARA REUTILIZAR SUBCONTA POR CPF/CNPJ USAR APENAS SE PLATAFORMA UTILIZAR SUBCONTA APENAS PARA UMA ENTIDADE</param>
        /// <returns></returns>
        Task<IuguBaseMarketPlace> SendVerifyOrUpdateDataBankAsync(DataBankViewModel model, bool newRegister, string liveKey = null, string userApiKey = null, string accountKey = null, long? lastVerification = null, string marketplaceName = null, bool useGoogleVerificationAddress = false, string googleKey = null, bool checkExists = false);
        /// <summary>
        /// ENVIAR CONVITE DE ACESSO A SUBCONTA
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userApiToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<InviteResponseViewModel> CreateInviteAccessSubAccountAsync(string accountId, string userApiToken, InviteViewModel model);
        /// <summary>
        /// LISTAR CONVITES ENVIADOS DA SUBCONTA
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userApiToken"></param>
        /// <returns></returns>
        Task<List<InviteResponseViewModel>> ListInvitesBySubAccountAsync(string accountId, string userApiToken);
        /// <summary>
        /// REENVIAR CONVITE
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userApiToken"></param>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        Task<InviteResponseViewModel> ResendInviteAsync(string accountId, string inviteId, string userApiToken);
        /// <summary>
        /// CANCELAR CONVITE
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userApiToken"></param>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        Task<InviteResponseViewModel> CancelInviteAsync(string accountId, string inviteId, string userApiToken);
        /// <summary>
        /// OBTER SUBCONTAS CADASTRADAS NA MASTER COM FILTRO POR ID DE UMA SUBCONTA
        /// </summary>
        /// <param name="subAccountId"></param>
        /// <returns></returns>
        Task<IuguMarketPlaceConfigResponse> GetCredentialsSubAccounts(string subAccountId = null);

        /// <summary>
        /// VALIDAR ASSINATURA DE CHAMADAS (OBRIGATÓRIO ASSINAR REQUEST)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        Task<ValidadeSignatureResponseViewModel> ValidateSignature(object model);
        /// <summary>
        /// DESATIVAR SUBCONTA
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<SimpleResponseMessage> DesativarSubContaAsync(IuguAccountCreateResponseModel model);
        /// <summary>
        /// CONSULTAR DOCUMENTOS
        /// </summary>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        Task<IuguDocumentsResponseModel> ConsultarDocumentosAsync(string apiToken);
        /// <summary>
        /// REENVIAR DOCUMENTOS DE VALIDAÇÃO DA SUBCONTA
        /// </summary>
        /// <param name="model"></param>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        Task<IuguDocumentsResponseModel> ReenviarDocumentosSubContaAsync(IuguAccountVerificationModel model, string apiToken);

    }
}