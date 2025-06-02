using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Stripe;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public partial class StripePaymentIntentService(IHostingEnvironment env) : StripeBaseService(env), IStripePaymentIntentService
    {

        public async Task<StripeBaseResponse<PaymentIntent>> GetByIdAsync(string id)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentIntentService();

                return await service.GetAsync(id);
            });
        }

        public async Task<StripeBaseResponse<StripePaymentIntentDetails>> GetPaymentIntentDetailsAsync(string paymentIntentId)
        {
            return await HandleActionAsync(async () =>
            {
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId);

                var paymentDetails = new StripePaymentIntentDetails
                {
                    PaymentStatus = paymentIntent.Status.MapPaymentStatus(),
                    ChargeId = paymentIntent.LatestChargeId,
                    TransferData = paymentIntent.TransferData,
                    GrossAmount = paymentIntent.Amount.ToDouble()
                };

                if (string.IsNullOrEmpty(paymentIntent.LatestChargeId) == false)
                {
                    var chargeService = new ChargeService();
                    var charge = await chargeService.GetAsync(paymentIntent.LatestChargeId);

                    paymentDetails.RiskScore = charge.Outcome?.RiskScore;
                    paymentDetails.RiskLevel = charge.Outcome?.RiskLevel;
                    paymentDetails.RiskDetails = charge.Outcome.GetRiskDetails();
                    paymentDetails.ReceiptUrl = charge.ReceiptUrl;
                    paymentDetails.AmountCaptured = charge.AmountCaptured.ToDouble();
                    paymentDetails.RefundedAmount = charge.AmountRefunded.ToDouble();
                    paymentDetails.Currency = charge.Currency.ToUpper();
                    paymentDetails.PaymentMethod = charge.PaymentMethodDetails;
                    paymentDetails.PaymentMethodType = $"{charge.PaymentMethodDetails?.Type}_{charge.PaymentMethodDetails?.Card?.Funding}".TrimEnd('_');
                    paymentDetails.PaymentMethodDetails = charge.GetPaymentMethodDetails();
                    paymentDetails.BalanceTransactionId = charge.BalanceTransactionId;
                    paymentDetails.TransferData ??= charge.TransferData;
                    paymentDetails.PaymentStatus = paymentIntent.Status.MapPaymentStatus(charge);

                    if (string.IsNullOrEmpty(charge.BalanceTransactionId) == false)
                    {
                        var balanceTransactionService = new BalanceTransactionService();
                        var balanceTransaction = await balanceTransactionService.GetAsync(charge.BalanceTransactionId);

                        paymentDetails.NetAmount = balanceTransaction.Net.ToDouble();
                        paymentDetails.ProcessingFee = balanceTransaction.Fee.ToDouble();
                        paymentDetails.FundsStatus = balanceTransaction.Status;
                        paymentDetails.AvailableOn = balanceTransaction.AvailableOn;
                    }
                }

                return paymentDetails;
            });
        }

        public async Task<StripeBaseResponse<PaymentIntent>> CreateAnonymousCreditCardPaymentAsync(StripeAnonymousPaymentRequest request)
        {
            return await HandleActionAsync(async () =>
            {
                var servicePaymentMethod = new PaymentMethodService();

                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = request.CreditCard.CardNumber,
                        ExpMonth = request.CreditCard.ExpMonth,
                        ExpYear = request.CreditCard.ExpYear,
                        Cvc = request.CreditCard.Cvv,
                    },
                };

                var paymentMethod = await servicePaymentMethod.CreateAsync(paymentMethodOptions);

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(request.Amount * 100),
                    Currency = request.Currency,
                    Description = request.Description,
                    PaymentMethod = paymentMethod.Id,
                    OffSession = true,
                    PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                    {
                        Card = new PaymentIntentPaymentMethodOptionsCardOptions
                        {
                            Installments = new PaymentIntentPaymentMethodOptionsCardInstallmentsOptions()
                            {
                                Enabled = true,
                                Plan = new()
                                {
                                    Count = request.Installments,
                                    Interval = "month",
                                    Type = "fixed_count"
                                }
                            }
                        }
                    },
                    Confirm = true,
                    PaymentMethodTypes = ["card"],
                    UseStripeSdk = false,
                    Metadata = []
                };

                if (!string.IsNullOrEmpty(request.Customer?.FullName))
                {
                    options.Metadata.Add("customer_name", request.Customer.FullName);
                }

                if (!string.IsNullOrEmpty(request.Customer?.Email))
                {
                    options.Metadata.Add("customer_email", request.Customer.Email);
                }

                var service = new PaymentIntentService();
                return await service.CreateAsync(options);
            });
        }

        public async Task<StripeBaseResponse<PaymentIntent>> CreateCreditCardPaymentAsync(StripeTransactionCreditCardRequest request)
        {
            return await HandleActionAsync(async () =>
            {

                if (string.IsNullOrEmpty(request.CustomerId))
                    throw new CustomError("CustomerId é obrigatório");

                if (string.IsNullOrEmpty(request.CreditCardId))
                    throw new CustomError("CreditCardId é obrigatório");

                var servicePaymentMethod = new PaymentMethodService();

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(request.Amount * 100),
                    Currency = request.Currency.ToLower(),
                    Description = request.Description,
                    PaymentMethod = request.CreditCardId,
                    Customer = request.CustomerId,
                    OffSession = true,
                    PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                    {
                        Card = new PaymentIntentPaymentMethodOptionsCardOptions
                        {
                            Installments = new PaymentIntentPaymentMethodOptionsCardInstallmentsOptions()
                            {
                                Enabled = true,
                                Plan = new()
                                {
                                    Count = request.Installments,
                                    Interval = "month",
                                    Type = "fixed_count"
                                }
                            }
                        }
                    },
                    Confirm = request.Capture,
                    PaymentMethodTypes = ["card"],
                };

                var service = new PaymentIntentService();
                return await service.CreateAsync(options);
            });
        }

        public async Task<StripeBaseResponse<PaymentIntent>> PixPaymentAsync(StripePixPaymentRequest request)
        {
            return await HandleActionAsync(async () =>
            {
                if (string.IsNullOrEmpty(request.CustomerId))
                {
                    throw new Exception("CustomerId é obrigatório");
                }

                request.DueDate ??= DateTime.Now.AddDays(1).AddSeconds(-1);

                var expires = (int)(DateTime.Now - request.DueDate.GetValueOrDefault()).TotalSeconds;

                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount.ToCent(),
                    Currency = request.Currency,
                    Customer = request.CustomerId,
                    Description = request.Description,
                    PaymentMethodTypes = ["pix"],
                    PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                    {
                        Pix = new PaymentIntentPaymentMethodOptionsPixOptions
                        {
                            ExpiresAt = request.DueDate
                        }
                    }
                };

                var service = new PaymentIntentService();

                return await service.CreateAsync(paymentIntentOptions);
            });
        }

        public async Task<StripeBaseResponse> BankSlipPaymentAsync(StripeBankSlipPaymentRequest request)
        {
            return await HandleActionAsync(async () =>
            {
                if (string.IsNullOrEmpty(request.CustomerId))
                {
                    throw new Exception("CustomerId é obrigatório");
                }

                if (request.Amount < 1)
                {
                    throw new Exception("O valor mínimo é R$ 1,00");
                }

                request.DueDate ??= DateTime.Now.AddDays(1).AddSeconds(-1);
                var days = (DateTime.Now - request.DueDate.GetValueOrDefault()).Days;

                var paymentIntentOptions = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount.ToCent(),
                    Currency = request.Currency,
                    Customer = request.CustomerId,
                    Description = request.Description,
                    PaymentMethodTypes = ["boleto"],
                    PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                    {
                        Boleto = new PaymentIntentPaymentMethodOptionsBoletoOptions
                        {
                            ExpiresAfterDays = Math.Max(1, days)
                        }
                    }
                };

                var service = new PaymentIntentService();

                return await service.CreateAsync(paymentIntentOptions);
            });
        }

        public async Task<StripeBaseResponse> RefundPaymentAsync(string paymentIntentId, long? amount = null)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentIntentId);

                if (paymentIntent.Status == "canceled")
                    return;

                var needCancel = new HashSet<string> { "requires_payment_method", "requires_confirmation", "requires_action" };
                if (needCancel.Contains(paymentIntent.Status))
                {
                    var cancel = await service.CancelAsync(paymentIntentId);
                    return;
                }

                var refundService = new RefundService();
                var options = new RefundCreateOptions
                {
                    PaymentIntent = paymentIntentId,
                    Amount = amount > 0 ? amount : null
                };

                var refund = await refundService.CreateAsync(options);
                return;
            });
        }

        public async Task<StripeBaseResponse<PaymentIntent>> CapturePaymentIntentAsync(string paymentIntentId, long? amountToCapture = null)
        {
            return await HandleActionAsync(async () =>
            {
                var options = new PaymentIntentCaptureOptions
                {
                    AmountToCapture = amountToCapture,
                };

                var service = new PaymentIntentService();
                return await service.CaptureAsync(paymentIntentId, options);
            });
        }
    }
}