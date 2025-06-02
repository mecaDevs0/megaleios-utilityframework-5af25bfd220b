using System.Threading.Tasks;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.Interface
{
    public interface IZoopTransactionService
    {
        ResponseTransactionViewModel TransactionWithTokenCard(RegisterTokenTransactionViewModel model, string marketPlaceId = null);
        ResponseTransactionViewModel TransactionWithCreditCard(RegisterCreditCardTransactionViewModel model, string marketPlaceId = null);
        ResponseTransactionViewModel TransactionWithBankSlip(BankSlipTransactionViewModel model, string marketPlaceId = null);
        ResponseTransactionViewModel Transaction(TransactionViewModel model, string marketPlaceId = null);
        ResponsePaymentMethodViewModel GetBankSlip(string banksplip_id, string marketPlaceId = null);
        Task<ResponsePaymentMethodViewModel> GetBankSlipAsync(string banksplip_id, string marketPlaceId = null, bool configureAwait = false);
        BaseResponseViewModel<object> ReverseTransaction(ReverseTransactionViewModel model, string transactionId, string marketPlaceId = null);
        Task<ResponseTransactionViewModel> TransactionAsync(TransactionViewModel model, string marketPlaceId = null, bool configureAwait = false);
        Task<ResponseTransactionViewModel> TransactionWithBankSlipAsync(BankSlipTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false);
        Task<ResponseTransactionViewModel> TransactionWithTokenCardAsync(RegisterTokenTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false);
        Task<ResponseTransactionViewModel> TransactionWithCreditCardAsync(RegisterCreditCardTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false);
        Task<BaseResponseViewModel<object>> ReverseTransactionAsync(ReverseTransactionViewModel model, string transactionId, string marketPlaceId = null, bool configureAwait = false);
    }
}