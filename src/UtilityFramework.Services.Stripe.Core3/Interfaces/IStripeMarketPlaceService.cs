using System.Threading.Tasks;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3.Interfaces
{
    public interface IStripeMarketPlaceService
    {
        /// <summary>
        /// CONTA PLATAFORMA (MASTER)
        /// </summary>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> GetPlatformAsync();
        /// <summary>
        /// LISTAR SUBCONTAS
        /// </summary>
        /// <returns></returns>
        Task<StripeBaseResponse<StripeList<Account>>> ListAsync();
        /// <summary>
        /// BUSCAR SUBCONTA PELO ID
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> GetByIdAsync(string accountId);
        /// <summary>
        /// DADOS DA CONTA EXTERNA VINCULADA
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<BankAccount>> GetBankAccountAsync(string accountId);
        /// <summary>
        /// CRIAR SUBCONTA COM DADOS LIMITADOS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="accountType"></param>
        /// <param name="capabilitiesOptions"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> CreateAsync(StripeMarketPlaceRequest data, EStripeAccountType accountType = EStripeAccountType.Express, AccountCapabilitiesOptions capabilitiesOptions = null);
        /// <summary>
        /// CADASTRAR SUBCONTA COM TODOS DADOS
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> CreateFullOptionsAsync(AccountCreateOptions options);
        /// <summary>
        /// ATUALIZAR SUBCONTA COM TODOS DADOS
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> UpdateAsync(string accountId, StripeMarketPlaceRequest data);
        /// <summary>
        /// REMOVER SUBCONTA
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> DeleteAsync(string accountId);

        /// <summary>
        /// REJEITAR SUBCONTA
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> RejectAsync(string accountId);

        /// <summary>
        /// METODO PARA MAPEAR DADOS DO BANCO PARA O STRIPE
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <returns></returns>
        AccountBankAccountOptions CreateBankAccountOptions(StripeExternalAccountMarketPlaceRequest bankAccount);

        /// <summary>
        /// CONFIGURAR DADOS DO REPRESENTANTE PARA EMPRESA
        /// </summary>
        /// <param name="representative"></param>
        /// <returns></returns>
        Task<AccountPersonCreateOptions> ConfigurePersonOptions(StripeIndividualRequest representative, bool skipDocumentIndividual = false);
        /// <summary>
        /// CONFIGURAR DADOS PARA PESSOA FÍSICA
        /// </summary>
        /// <param name="options"></param>
        /// <param name="individual"></param>
        /// <param name="skipUploadDocument"></param>
        /// <returns></returns>
        Task ConfigureIndividualOptions(AccountCreateOptions options, StripeIndividualRequest individual, bool skipUploadDocument = false);
        /// <summary>
        /// CONFIGURAR DADOS DA EMPRESA
        /// </summary>
        /// <param name="options"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        Task ConfigureCompanyOptions(AccountCreateOptions options, StripeCompanyRequest company, bool skipDocumentCompany = false);
        /// <summary>
        /// FAZER UPLOAD DE ARQUIVOS PARA VERIFICACAO DE ACCOUNT
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filePurpose"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<File>> UploadFileAsync(string path, EStripeFilePurpose filePurpose = EStripeFilePurpose.IdentityDocument);
        /// <summary>
        /// ATUALIZAR DADOS BANCÁRIOS DA CONTA
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="dataBankOptions"></param>
        /// <returns></returns>
        Task<StripeBaseResponse<Account>> UpdateExternalAccountAsync(string accountId, AccountBankAccountOptions dataBankOptions);
    }
}