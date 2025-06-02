using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.WebApi.Core3.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ValidationController : MainController
    {

        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripePaymentIntentService _stripePaymentService;
        private readonly IStripeTransferService _stripeTransferService;
        private readonly IStripeMarketPlaceService _stripeMarketPlaceService;

        public ValidationController(IStripeCustomerService stripeCustomerService,
                                    IStripePaymentMethodService stripePaymentMethodService,
                                    IStripePaymentIntentService stripePaymentService,
                                    IStripeTransferService stripeTransferService,
                                    IStripeMarketPlaceService stripeMarketPlaceService)
        {
            _stripeCustomerService = stripeCustomerService;
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripePaymentService = stripePaymentService;
            _stripeTransferService = stripeTransferService;
            _stripeMarketPlaceService = stripeMarketPlaceService;
        }

        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount([FromBody] StripeMarketPlaceRequest request)
        {
            return await HandleActionAsync(async () =>
            {
                request.UserAgent = Request.Headers["User-Agent"].ToString();
                request.AcceptTerms = true;
                request.RemoteIp = Utilities.GetClientIp();

                var subAccount = await _stripeMarketPlaceService.CreateAsync(request);
                return subAccount;
            });
        }
        [HttpGet("GetAccount/{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] string id)
        {
            return await HandleActionAsync(async () =>
            {

                return await _stripeMarketPlaceService.GetByIdAsync(id);
            });
        }
        [HttpPost("UpdateAccount/{id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] string id, [FromBody] AccountUpdateOptions request)
        {
            return await HandleActionAsync(async () =>
            {
                var subAccount = await _stripeMarketPlaceService.UpdateAsync(id, request);
                return subAccount;
            });
        }
        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string id)
        {
            return await HandleActionAsync(async () =>
            {
                var subAccount = await _stripeMarketPlaceService.DeleteAsync(id);
                return subAccount;
            });
        }


        [HttpGet("GetCards/{id}")]
        public async Task<IActionResult> GetCards([FromRoute] string id)
        {
            return await HandleActionAsync(async () =>
            {
                var stripeResult = await _stripePaymentMethodService.ListCreditCardAsync(id);

                return stripeResult?.Data.Data.ToList() ?? [];
            });
        }
        [HttpGet("CardAttach/{customerId}/{paymentMethodId}")]
        public async Task<IActionResult> CardAttach([FromRoute] string paymentMethodId, [FromRoute] string customerId)
        {
            return await HandleActionAsync(async () =>
            {
                var result = await _stripePaymentMethodService.LinkCreditCardToCustomerAsync(customerId, paymentMethodId);

                if (!result.Success)
                {
                    throw new CustomError(result.ErrorMessage);
                }
                return;
            });
        }
        [HttpPost("Charge")]
        public async Task<IActionResult> Charge([FromBody] PaymentCreateRequest request)
        {

            return await HandleActionAsync(async () =>
            {

                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount.ToCent(),
                    Currency = "brl",
                    Customer = request.CustomerId,
                    Metadata = new Dictionary<string, string>
                    {
                        { "installments", request.Installments.ToString() }
                    }
                };

                switch (request.PaymentMethodType.ToLower())
                {
                    case "card":
                        options.Confirm = request.Capture;
                        options.PaymentMethod = request.SelectedCardId;
                        options.PaymentMethodTypes = ["card"];
                        options.PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                        {
                            Card = new PaymentIntentPaymentMethodOptionsCardOptions
                            {
                                Installments = new PaymentIntentPaymentMethodOptionsCardInstallmentsOptions
                                {
                                    Enabled = true,
                                    Plan = new PaymentIntentPaymentMethodOptionsCardInstallmentsPlanOptions()
                                    {
                                        Count = request.Installments,
                                        Type = "fixed_count",
                                        Interval = "month"
                                    }
                                }
                            }
                        };
                        break;

                    case "pix":
                        options.PaymentMethodTypes = ["pix"];
                        options.PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                        {
                            Pix = new PaymentIntentPaymentMethodOptionsPixOptions
                            {
                                ExpiresAfterSeconds = 3600
                            }
                        };
                        break;

                    case "boleto":
                        options.PaymentMethodTypes = ["boleto"];
                        options.PaymentMethodOptions = new PaymentIntentPaymentMethodOptionsOptions
                        {
                            Boleto = new PaymentIntentPaymentMethodOptionsBoletoOptions
                            {
                                ExpiresAfterDays = 3
                            }
                        };
                        break;

                    default:
                        throw new CustomError("Método de pagamento inválido");
                }

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return new PaymentResponse
                {
                    PaymentIntentId = paymentIntent.Id,
                    ClientSecret = paymentIntent.ClientSecret,
                    Details = GetPaymentDetails(paymentIntent)
                };
            });
        }

        private static PaymentIntentPaymentMethodOptionsCardInstallmentsPlanOptions GetInstallmentPlan(int installments, decimal amount)
        {
            return installments switch
            {
                > 2 and <= 12 when amount >= 100 => new PaymentIntentPaymentMethodOptionsCardInstallmentsPlanOptions
                {
                    Type = "fixed_count",
                    Count = installments,
                    Interval = "month"
                },
                _ => new PaymentIntentPaymentMethodOptionsCardInstallmentsPlanOptions { Type = "fixed_count", Count = 1, Interval = "month" }
            };
        }

        private object GetPaymentDetails(PaymentIntent paymentIntent)
        {
            return paymentIntent.Status switch
            {
                "requires_action" when paymentIntent.NextAction?.Type == "display_pix_details" => new
                {
                    Type = "pix",
                    QrCode = paymentIntent.NextAction.PixDisplayQrCode.Data,
                    ExpiresAt = paymentIntent.NextAction.PixDisplayQrCode?.ExpiresAt
                },
                "requires_action" when paymentIntent.NextAction?.Type == "display_boleto_details" => new
                {
                    Type = "boleto",
                    Url = paymentIntent.NextAction.BoletoDisplayDetails?.HostedVoucherUrl,
                    Barcode = paymentIntent.NextAction.BoletoDisplayDetails?.RawJObject,
                    ExpiresAt = paymentIntent.NextAction.BoletoDisplayDetails?.ExpiresAt
                },
                "requires_confirmation" when paymentIntent.PaymentMethod?.Type == "card" => new
                {
                    Type = "card",
                    Installments = paymentIntent.Metadata["installments"]
                },
                _ => null
            };
        }
    }

    public class StripeCustomerRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    public class PaymentCreateRequest
    {
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethodType { get; set; }
        public string SelectedCardId { get; set; }
        public int Installments { get; set; } = 1;
        public bool Capture { get; internal set; } = true;
    }

    public class PaymentResponse
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public string NextAction { get; set; }
        public object Details { get; set; }
    }
}