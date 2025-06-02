using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Stripe;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class StripeTransferService(IHostingEnvironment env) : StripeBaseService(env), IStripeTransferService
    {
        public async Task<StripeBaseResponse<Transfer>> CreateTransferAsync(StripeTransferRequest request)
        {
            return await HandleActionAsync(async () =>
            {
                var result = await CanTransfer(request.Amount, request.SourceTransactionId);
                if (!result.Data)
                {
                    throw new CustomError("Saldo insuficiente para realizar essa transferência.");
                }

                var options = new TransferCreateOptions
                {
                    Amount = request.Amount.ToCent(),
                    Currency = request.Currency.ToLower(),
                    Destination = request.SubAccountId,
                    SourceTransaction = request.SourceTransactionId,
                    Description = request.Description,
                };

                var service = new TransferService();
                return await service.CreateAsync(options);
            });
        }
        public async Task<StripeBaseResponse<Transfer>> CreateTransferFullDataAsync(TransferCreateOptions options)
        {
            return await HandleActionAsync(async () =>
            {

                var result = await CanTransfer(options.Amount.GetValueOrDefault().ToDouble(), options.SourceTransaction);
                if (!result.Data)
                {
                    throw new CustomError("Saldo insuficiente para realizar essa transferência.");
                }

                var service = new TransferService();
                return await service.CreateAsync(options);
            });
        }

        public async Task<StripeBaseResponse<long>> GetAvailableBalanceAsync(string accountId = null, string currency = "BRL")
        {
            return await HandleActionAsync(async () =>
            {
                var service = new BalanceService();
                Balance balance;

                if (string.IsNullOrEmpty(accountId))
                {
                    balance = await service.GetAsync();
                }
                else
                {
                    balance = await service.GetAsync(new RequestOptions
                    {
                        StripeAccount = accountId,
                    });
                }

                var available = balance.Available
                    .Where(b => b.Currency.EqualsIgnoreCase(currency))
                    .Sum(b => b.Amount);

                return available;
            });
        }


        private async Task<StripeBaseResponse<bool>> CanTransfer(double requiredAmount, string accountId = null, string currency = "BRL")
        {
            return await HandleActionAsync(async () =>
            {
                var result = await GetAvailableBalanceAsync(accountId, currency);

                if (result.Data >= requiredAmount.ToCentLong())
                    return true;

                var money = result.Data.ToDouble().ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
                throw new CustomError(string.Format("Saldo insuficiente, o saldo disponível é {0}", money));
            });
        }
    }
}