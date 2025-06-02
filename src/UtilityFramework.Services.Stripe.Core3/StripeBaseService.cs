using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Stripe;

using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class StripeBaseService
    {
        public StripeBaseService(IHostingEnvironment env)
        {
            Init(env);
        }

        public void Init(IHostingEnvironment env)
        {
            StripeConfiguration.ApiKey ??= Utilities.GetConfigurationRoot(environment: env).GetSection("Stripe:SecretKey").Get<string>();

            if (string.IsNullOrEmpty(StripeConfiguration.ApiKey))
            {
                throw new Exception("Stripe:SecretKey não encontrada no appsettings.{env}.json");
            }
        }

        public static async Task<StripeBaseResponse<T>> HandleActionAsync<T>(
            Func<Task<T>> paymentAction)
        {
            var response = new StripeBaseResponse<T>();

            try
            {
                var result = await paymentAction();

                response.Success = true;
                response.Data = result;
            }
            catch (StripeException ex)
            {
                response.ErrorMessage = $"[Stripe Error] {ex.StripeError.Message}";
                response.ErrorCode = ex.StripeError.Code;
                response.ErrorType = ex.StripeError.Type;
                response.ErrorParam = ex.StripeError.Param;
                response.StackTrace = ex.StackTrace;
            }
            catch (CustomError ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorType = "custom_error";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.StackTrace = ex.StackTrace;
                response.ErrorType = "exception";
            }

            return response;
        }
        public static async Task<StripeBaseResponse> HandleActionAsync(
            Func<Task> paymentAction)
        {
            var response = new StripeBaseResponse();

            try
            {
                await paymentAction();
                response.Success = true;
            }
            catch (StripeException ex)
            {
                response.Success = false;
                response.ErrorMessage = $"[Stripe Error] {ex.StripeError.Message}";
                response.ErrorCode = ex.StripeError.Code;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                response.StackTrace = ex.StackTrace;
            }

            return response;
        }
    }
}