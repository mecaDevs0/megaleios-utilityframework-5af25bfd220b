using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using AutoMapper.Internal;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using UtilityFramework.Application.Core3;
using UtilityFramework.Application.Core3.ViewModels;
using UtilityFramework.Services.Iugu.Core3.Entity;
using UtilityFramework.Services.Iugu.Core3.Entity.Lists;
using UtilityFramework.Services.Iugu.Core3.Interface;
using UtilityFramework.Services.Iugu.Core3.Models;
using UtilityFramework.Services.Iugu.Core3.Request;
using UtilityFramework.Services.Iugu.Core3.Response;

namespace UtilityFramework.Services.Iugu.Core3
{
    public class IuguService : IIuguCustomerServices, IIuguPaymentMethodService, IIuguChargeServices, IIuguMarketPlaceServices, IIuguService
    {
        private const string BaseUrl = "https://api.iugu.com/v1";
        private readonly IuguCredentials _iuguCredentials;
        private static IConfigurationRoot Configuration { get; set; }


        public IuguService(IHostingEnvironment environment = null)
        {
            Configuration ??= Utilities.GetConfigurationRoot(environment: environment);

            _iuguCredentials = Configuration.GetSection("IUGU").Get<IuguCredentials>();

            if (string.IsNullOrEmpty(_iuguCredentials?.AccoundId))
                _iuguCredentials = Utilities.GetConfigurationRoot().GetSection("IUGU").Get<IuguCredentials>();

            _iuguCredentials.SetKeyUsage();
        }

        public async Task<IuguCustomerModel> SaveClientAsync(IuguCustomerModel customer)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/customers")
                {
                    Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(customer);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguCustomerModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguCustomerModel();

                var errors = retorno.IuguMapErrors<IuguCustomerModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguCustomerModel> UpdateClientAsync(string clientId, IuguCustomerModel customer)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/customers/{clientId}")
                {
                    Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.PUT);

                var json = JsonConvert.SerializeObject(customer);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguCustomerModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguCustomerModel();

                var errors = retorno.IuguMapErrors<IuguCustomerModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task DeleteClientAsync(string id)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/customers/{id}")
                {
                    Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.DELETE);

                var response = await client.ExecuteAsync(request).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Erro ao remover cliente :  {response.Content}");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguCustomerModel> SetDefaultCartaoAsync(string clientId, string cardId, string cpfCnpj)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/customers/{clientId}")
                {
                    Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.PUT);

                var model = new
                {
                    default_payment_method_id = cardId,
                    cpf_cnpj = cpfCnpj
                };
                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguCustomerModel>(request).ConfigureAwait(false);
                var _return = retorno.Data ?? new IuguCustomerModel();

                var errors = retorno.IuguMapErrors<IuguCustomerModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguCustomerModel> SetDefaultCartaoAsync(string clientId, string cardId)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/customers/{clientId}")
                {
                    Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.PUT);

                var model = new
                {
                    default_payment_method_id = cardId,
                    //cpf_cnpj = cpfCnpj
                };
                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguCustomerModel>(request).ConfigureAwait(false);
                var _return = retorno.Data ?? new IuguCustomerModel();

                var errors = retorno.IuguMapErrors<IuguCustomerModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguPaymentMethodToken> SaveCreditCardAsync(IuguPaymentMethodToken model)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/payment_token");
                var request = new RestRequest(Method.POST);
                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                model.AccountId = _iuguCredentials.AccoundId;
                var json = JsonConvert.SerializeObject(model);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguPaymentMethodToken>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguPaymentMethodToken();

                var errors = retorno.IuguMapErrors<IuguPaymentMethodToken>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IEnumerable<IuguCreditCard>> ListarCredCardsAsync(string clientId)
        {
            try
            {
                var url = $"{BaseUrl}/customers/{clientId}/payment_methods";
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<IEnumerable<IuguCreditCard>>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguCreditCard> LinkCreditCardClientAsync(IuguCustomerPaymentMethod model, string clienteId)
        {
            try
            {
                var url = $"{BaseUrl}/customers/{clienteId}/payment_methods";
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguCreditCard>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguCreditCard();

                var errors = retorno.IuguMapErrors<IuguCustomerPaymentMethod>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguCreditCard> BuscarCredCardsAsync(string clientId, string cardId)
        {
            try
            {
                var url = $"{BaseUrl}/customers/{clientId}/payment_methods/{cardId}";
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<IuguCreditCard>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguCreditCard();

                var errors = retorno.IuguMapErrors<IuguCreditCard>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguCreditCard> RemoverCredCardAsync(string clientId, string cardId)
        {
            try
            {
                var url = $"{BaseUrl}/customers/{clientId}/payment_methods/{cardId}";
                var client = new RestClient(url);
                var request = new RestRequest(Method.DELETE);
                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<IuguCreditCard>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguCreditCard();

                var errors = retorno.IuguMapErrors<IuguCreditCard>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguInvoiceResponseMessage> GerarFaturaAsync(InvoiceRequestMessage model, string apiToken = null)
        {
            try
            {
                var client = new RestClient("https://api.iugu.com/v1/invoices");
                var request = new RestRequest(Method.POST);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguInvoiceResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguInvoiceResponseMessage();

                var errors = retorno.IuguMapErrors<InvoiceRequestMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguInvoiceResponseMessage> CancelarFaturaAsync(string invoiceId, string apiToken = null)
        {
            try
            {

                var client = new RestClient($"{BaseUrl}/invoices/{invoiceId}/cancel");
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");
                var request = new RestRequest(Method.PUT);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<IuguInvoiceResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguInvoiceResponseMessage();

                var errors = retorno.IuguMapErrors<IuguInvoiceResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {

                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguChargeResponse> TransacaoCreditCardAsync(IuguChargeRequest model, string clienteId, string idCartao = null, string token = null, string apiToken = null)
        {
            try
            {
                if (string.IsNullOrEmpty(apiToken))
                    apiToken = _iuguCredentials.KeyUsage;

                var client = new RestClient($"{BaseUrl}/charge");
                client.Authenticator = new HttpBasicAuthenticator(apiToken, "");
                var request = new RestRequest(Method.POST);
                object newModel;



                if (string.IsNullOrEmpty(idCartao) == false)
                {
                    newModel = new
                    {
                        api_token = apiToken,
                        email = model.Email,
                        customer_payment_method_id = idCartao,
                        customer_id = clienteId,
                        Months = model.Months ?? 1,
                        items = model.InvoiceItems,
                    };
                }
                else
                {
                    newModel = new
                    {
                        api_token = apiToken,
                        email = model.Email,
                        token = token,
                        Months = model.Months ?? 1,
                        customer_id = clienteId,
                        items = model.InvoiceItems,
                    };
                }
                var json = JsonConvert.SerializeObject(newModel);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguChargeResponse>(request).ConfigureAwait(false);
                var _return = retorno.Data ?? new IuguChargeResponse();

                var errors = retorno.IuguMapErrors<IuguChargeRequest>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError || _return.Success == false;

                if (!string.IsNullOrEmpty(_return?.LR) && _return.LR.SuccessTransaction() == false)
                {
                    _return.MsgLR = IuguUtility.STATUS_LR(_return.LR);
                    _return.HasError = true;
                    _return.MessageError = _return.MsgLR;
                }

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguInvoiceResponseMessage> GetFaturaAsync(string invoiceId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/invoices/{invoiceId}");
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");
                var request = new RestRequest(Method.GET);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<IuguInvoiceResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguInvoiceResponseMessage();

                var errors = retorno.IuguMapErrors<IuguInvoiceResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguChargeResponse> EstornarFaturaAsync(string invoiceId, int? refundCents = null, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/invoices/{invoiceId}/refund");
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                if (refundCents != null && refundCents > 1)
                {
                    var json = JsonConvert.SerializeObject(new IuguRefundModel() { PartialValueRefundCents = refundCents.GetValueOrDefault() });

                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                }

                var retorno = await client.ExecuteAsync<IuguChargeResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguChargeResponse();

                var errors = retorno.IuguMapErrors<IuguChargeResponse>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError || _return.Success == false;

                if (!string.IsNullOrEmpty(_return?.LR) && _return.LR.SuccessTransaction() == false)
                {
                    _return.MsgLR = IuguUtility.STATUS_LR(_return.LR);
                    _return.HasError = true;
                    _return.MessageError = _return.MsgLR;
                }

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> GerarAssinaturaAsync(SubscriptionRequestMessage model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/subscriptions")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionRequestMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> BuscarAssinaturaAsync(string subscriptionId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/subscriptions/{subscriptionId}")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.GET);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> RemoverAssinaturaAsync(string subscriptionId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/subscriptions/{subscriptionId}")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.DELETE);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> AlterarPlanoDaAssinaturaAsync(string subscriptionId, string planIdentifier, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/subscriptions/{subscriptionId}/change_plan/{planIdentifier}")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> AtivarAsinaturaAsync(string signatureId, string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/subscriptions/{signatureId}/activate";
                var client = new RestClient(url);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");
                var request = new RestRequest(Method.POST);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SubscriptionResponseMessage> SuspenderAssinaturaAsync(string signatureId, string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/subscriptions/{signatureId}/suspend";
                var client = new RestClient(url);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");
                var request = new RestRequest(Method.POST);

                var retorno = await client.ExecuteAsync<SubscriptionResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new SubscriptionResponseMessage();

                var errors = retorno.IuguMapErrors<SubscriptionResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguPlanModel> CriarPlanoAsync(IuguPlanModel model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/plans")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguPlanModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguPlanModel();

                var errors = retorno.IuguMapErrors<IuguPlanModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguPlanModel> UpdatePlanoAsync(string planId, IuguPlanModel model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/plans/" + planId)
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.PUT);

                var json = JsonConvert.SerializeObject(model);
                json = json?.Replace("\"identifier\":null,", "");

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguPlanModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguPlanModel();

                var errors = retorno.IuguMapErrors<IuguPlanModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task RemoverPlanoAsync(string planId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/plans/{planId}")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.DELETE);

                var response = await client.ExecuteAsync(request).ConfigureAwait(false);

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Ocorreu um erro ao remover o plano");
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<ListIuguAdvanceResponse> GetRecebiveisAsync(int? start = null, int? limit = null, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/financial_transaction_requests")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.GET);

                if (limit != null)
                    request.AddParameter("limit", limit, ParameterType.GetOrPost);
                if (start != null)
                    request.AddParameter("start", start, ParameterType.GetOrPost);

                var retorno = await client.ExecuteAsync<ListIuguAdvanceResponse>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguAdvanceSimulationResponse> SimularAntecipacaoAsync(IuguAdvanceRequest model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/financial_transaction_requests/advance_simulation")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };
                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguAdvanceSimulationResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguAdvanceSimulationResponse();

                var errors = retorno.IuguMapErrors<IuguAdvanceRequest>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguAdvanceSimulationResponse> SolicitarAntecipacaoAsync(IuguAdvanceRequest model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/financial_transaction_requests/advance")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };
                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguAdvanceSimulationResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguAdvanceSimulationResponse();

                var errors = retorno.IuguMapErrors<IuguAdvanceRequest>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }



        public async Task<IuguAccountCreateResponseModel> CriarSubContaAsync(IuguAccountRequestModel model)
        {
            try
            {
                model.ApiToken ??= _iuguCredentials.LiveKeyRSA;

                return await SendRequestAsync<IuguAccountCreateResponseModel>(new()
                {
                    Url = $"{BaseUrl}/marketplace/create_account",
                    Method = Method.POST,
                    Data = model,
                    ApiToken = model.ApiToken,
                    NeedSignature = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguVerifyAccountModel> VerificarSubContaAsync(IuguAccountVerificationModel model, string apiToken, string accountId)
        {
            try
            {

                apiToken ??= _iuguCredentials.LiveKeyRSA;
                model.AutomaticValidation = model.Data.AutomaticValidation;

                var _return = await SendRequestAsync<IuguVerifyAccountModel>(new()
                {
                    Url = $"{BaseUrl}/accounts/{accountId}/request_verification",
                    Method = Method.POST,
                    Data = model,
                    ApiToken = apiToken,
                    NeedSignature = true
                });

                _return.AlreadyVerified = _return.Error.ToDictionary<List<string>>().CheckAlreadyVerified();

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguDocumentsResponseModel> ReenviarDocumentosSubContaAsync(IuguAccountVerificationModel model, string apiToken)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/account/documents");
                client.Authenticator = new HttpBasicAuthenticator(apiToken, "");
                var request = new RestRequest(Method.PUT);

                model.AutomaticValidation = null;
                model.Data = null;

                var json = JsonConvert.SerializeObject(model, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);


                var retorno = await client.ExecuteAsync<IuguDocumentsResponseModel>(request).ConfigureAwait(false);
                var _return = retorno.Data ?? new IuguDocumentsResponseModel();

                var errors = retorno.IuguMapErrors<IuguDocumentsResponseModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguDocumentsResponseModel> ConsultarDocumentosAsync(string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/account/documents")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken, "")
                };

                var request = new RestRequest(Method.GET);

                var retorno = await client.ExecuteAsync<IuguDocumentsResponseModel>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SimpleResponseMessage> DesativarSubContaAsync(IuguAccountCreateResponseModel model)
        {
            try
            {
                return await SendRequestAsync<SimpleResponseMessage>(new()
                {
                    Url = $"{BaseUrl}/marketplace/deactivate",
                    Method = Method.POST,
                    Data = model,
                    NeedSignature = false
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<SimpleResponseMessage> AtualizarDadosBancariosSubContaAsync(IuguUpdateDataBank model, string userApiToken)
        {
            try
            {
                userApiToken ??= _iuguCredentials.LiveKeyRSA;

                return await SendRequestAsync<SimpleResponseMessage>(new SendRequest()
                {
                    Url = $"{BaseUrl}/bank_verification",
                    Data = model,
                    Method = Method.POST,
                    NeedSignature = true,
                    ApiToken = userApiToken
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<List<IuguBankVerificationResponse>> VerificarAtualizacaoDadosBancariosSubContaAsync(string userApiToken)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/bank_verification/");
                client.Authenticator = new HttpBasicAuthenticator(userApiToken, "");
                var request = new RestRequest(Method.GET);

                var retorno = await client.ExecuteAsync<List<IuguBankVerificationResponse>>(request).ConfigureAwait(false);
                var _return = retorno.Data ?? new List<IuguBankVerificationResponse>();

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguAccountCompleteModel> GetInfoSubContaAsync(string accoutId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/accounts/{accoutId}").UseSerializer(new JsonNetSerializer());
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<IuguAccountCompleteModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguAccountCompleteModel();

                var errors = retorno.IuguMapErrors<IuguAccountCompleteModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);



                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguAccountCompleteModel> ConfigurarSubContaAsync(AccountConfigurationRequestMessage configurationRequest, string apiToken = null)
        {
            try
            {
                apiToken ??= _iuguCredentials.LiveKeyRSA;

                return await SendRequestAsync<IuguAccountCompleteModel>(new()
                {
                    Url = $"{BaseUrl}/accounts/configuration",
                    Method = Method.POST,
                    Data = configurationRequest,
                    ApiToken = apiToken,
                    NeedSignature = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguMarketPlaceResponse> GetAccountsAsync(IuguAccountFilterModel filter, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/marketplace".AddQueryStringParameters(filter));

                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<IuguMarketPlaceResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguMarketPlaceResponse();

                var errors = retorno.IuguMapErrors<IuguMarketPlaceResponse>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguVerifyAccountModel> SendRequestVerificationAsync(DataBankViewModel model, string userApiKey, string accountKey, bool useGoogleVerificationAddress = false, string googleKey = null)
        {
            AddressViewModel address = null;

            if (useGoogleVerificationAddress)
                address = await Utilities.GetInfoFromAdressLocation(model.Address, googleKey);

            model = await model.SetBankMask();

            return await VerificarSubContaAsync(new IuguAccountVerificationModel()
            {
                Data = new IuguAccountDataVerificationModel()
                {
                    Name = model.FantasyName,
                    CompanyName = model.SocialName,
                    Bank = model.Bank,
                    BankAg = model.BankAgency,
                    Address = model.Address,
                    AccountType = model.AccountType,
                    AutomaticTransfer = model.AutomaticTransfer,
                    AutomaticValidation = model.AutomaticValidation,
                    BankCc = model.BankAccount,
                    BusinessType = model.BusinessType,
                    PersonType = model.PersonType,
                    Cnpj = model.Cnpj,
                    RespCpf = model.AccountableCpf,
                    PriceRange = model.PriceRange,
                    Telphone = model.AccountablePhone,
                    RespName = model.AccountableName,
                    City = useGoogleVerificationAddress ? address?.City ?? model.City : model.City,
                    State = useGoogleVerificationAddress ? address?.State ?? model.State : model.State,
                    PhysicalProducts = model.PhysicalProducts,
                    Cep = model.Cep,
                    Cpf = model.Cpf
                },
                Files = model.Files
            }, userApiKey, accountKey);

        }

        public async Task<SimpleResponseMessage> UpdateDataBankAsync(DataBankViewModel model, string liveKey)
        {
            return await AtualizarDadosBancariosSubContaAsync(new IuguUpdateDataBank()
            {
                AccountType = model.AccountType.GetTypeAccout(),
                Bank = model.BankCode,
                AutomaticValidation = model.AutomaticValidation.ToString(),
                Agency = model.BankAgency,
                Account = model.BankAccount
            }, liveKey).ConfigureAwait(false);
        }

        public async Task<IuguBaseMarketPlace> SendVerifyOrUpdateDataBankAsync(DataBankViewModel model, bool newRegister, string liveKey = null, string userApiKey = null, string accountKey = null, long? lastVerification = null, string marketplaceName = null, bool useGoogleVerificationAddress = false, string googleKey = null, bool checkExists = false)
        {
            var response = new IuguBaseMarketPlace();
            try
            {
                model = await model.SetBankMask();

                var today = new DateTimeOffset(DateTime.Today).ToUnixTimeSeconds();

                if (lastVerification != null && lastVerification >= today)
                {
                    response.HasError = true;
                    response.MessageError = "Não e possível atualizar os dados bancários, já existe uma verificação de dados bancários em aberto.";

                    return response;
                }

                if (newRegister && string.IsNullOrEmpty(liveKey) && string.IsNullOrEmpty(userApiKey) && string.IsNullOrEmpty(accountKey))
                {
                    if (string.IsNullOrEmpty(marketplaceName))
                    {
                        response.HasError = true;
                        response.MessageError = "Informe o nome do market place";

                        return response;
                    }

                    IuguAccountCreateResponseModel iuguResponse = null;

                    if (checkExists)
                    {
                        iuguResponse = await GetSubAccountByDocument(model);
                    }

                    if (checkExists == false || iuguResponse.HasError)
                    {
                        iuguResponse = await CriarSubContaAsync(new IuguAccountRequestModel()
                        {
                            Name = marketplaceName,
                            CommissionPercent = 0,
                            Splits = model.Splits
                        }).ConfigureAwait(false);
                    }

                    if (iuguResponse.HasError == false)
                    {
                        response.AccountKey = accountKey = iuguResponse.AccountId;
                        response.LiveKey = liveKey = iuguResponse.LiveApiToken;
                        response.UserApiKey = userApiKey = iuguResponse.UserToken;
                        response.TestKey = iuguResponse.TestApiToken;
                        response.IsNewRegister = true;
                    }
                    else
                    {
                        response = response.MapErrorDataBank(iuguResponse);
                        return response;
                    }

                }
                else if (string.IsNullOrEmpty(liveKey) && string.IsNullOrEmpty(userApiKey) && string.IsNullOrEmpty(accountKey))
                {
                    response.HasError = true;
                    response.MessageError = "Verifique as chaves informadas do marketplace";

                    return response;
                }

                var hasSendVerification = false;
                if (string.IsNullOrEmpty(accountKey) == false)
                {
                    var marketPlace = await GetInfoSubContaAsync(accountKey, liveKey);

                    hasSendVerification = marketPlace.HasError == false && marketPlace?.LastVerificationRequestStatus != null;
                }

                if (model.LastRequestVerification != null && hasSendVerification && model.HasDataBank())
                {
                    response = await InternalUpdateDataBankAsync(response, model, model.BankAccount, model.BankAccount, liveKey).ConfigureAwait(false);
                }
                else
                {
                    // REGISTRO DE DADOS BANCARIOS
                    var iuguVerifyAccountModel = await SendRequestVerificationAsync(model, userApiKey, accountKey, useGoogleVerificationAddress, googleKey).ConfigureAwait(false);

                    if (iuguVerifyAccountModel.HasError)
                    {
                        if (iuguVerifyAccountModel.AlreadyVerified)
                        {
                            // SUBCONTA JA FOI VERIFICADA ENVIAR UPDATE DE DADOS BANCÁRIOS
                            response = await InternalUpdateDataBankAsync(response, model, model.BankAccount, model.BankAgency, liveKey).ConfigureAwait(false);
                        }
                        else
                        {
                            response = response.MapErrorDataBank(iuguVerifyAccountModel);
                        }

                        return response;
                    }
                    var now = DateTimeOffset.Now.ToUnixTimeSeconds();

                    response.InVerification = now;
                    response.UpdateDataBank = now;
                    response.AccoutableCpf = model.AccountableCpf;
                    response.AccoutableName = model.AccountableName;
                    response.AccountType = model.AccountType;
                    response.Bank = model.BankCode;
                    response.BankName = model.Bank;
                    response.BankAccount = model.BankAccount;
                    response.BankAgency = model.BankAgency;
                    response.PersonType = model.PersonType;
                    response.Cnpj = model.Cnpj;
                    response.CustomMessage = "Dados bancários atualizados com sucesso.";

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Error = new { error = $"{ex.InnerException} {ex.Message}".Trim() };
            }
            return response;
        }


        public async Task<List<WebHookViewModel>> GetWebhookLogAsync(string invoiceId, string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/web_hook_logs/{invoiceId}";
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<List<WebHookViewModel>>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<WebHookViewModel> ResendWebhookAsync(string webhookId, string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/web_hook_logs/{webhookId}/retry";
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<WebHookViewModel>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        private async Task<IuguBaseMarketPlace> InternalUpdateDataBankAsync(IuguBaseMarketPlace response, DataBankViewModel model, string bankAccount, string agencyAccount, string liveKey)
        {
            try
            {
                // ATUALIZAÇÃO DE DADOS BANCÁRIOS
                var responseIugu = await UpdateDataBankAsync(model, liveKey).ConfigureAwait(false);

                if (responseIugu.Success == false || responseIugu.HasError == true)
                {
                    response = response.MapErrorDataBank(responseIugu);

                    if (string.IsNullOrEmpty(response.MessageError))
                        response.MessageError = "Não foi possível atualizar os dados bancários, verifique os dados e tente novamente.";
                    return response;
                }
                else
                {
                    var now = DateTimeOffset.Now.ToUnixTimeSeconds();

                    response.InVerification = now;
                    response.UpdateDataBank = now;
                    response.AccountType = model.AccountType;
                    response.Bank = model.Bank;
                    response.BankAccount = model.BankAccount;
                    response.BankAgency = model.BankAgency;
                    response.Cnpj = model.Cnpj;

                    response.CustomMessage = "Dados bancários atualizados com sucesso";

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.MessageError = "Ocorreu um erro inesperado";
                response.Error = new { error = $"{ex.InnerException} {ex.Message}".Trim() };

                return response;
            }
        }

        public async Task<InviteResponseViewModel> CreateInviteAccessSubAccountAsync(string accountId, string userApiToken, InviteViewModel model)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/{accountId}/user_invites")
                {
                    Authenticator = new HttpBasicAuthenticator(userApiToken, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<InviteResponseViewModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new InviteResponseViewModel();

                var errors = retorno.IuguMapErrors<InviteResponseViewModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<List<InviteResponseViewModel>> ListInvitesBySubAccountAsync(string accountId, string userApiToken)
        {
            try
            {
                var url = $"{BaseUrl}/{accountId}/user_invites";

                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                client.Authenticator = new HttpBasicAuthenticator(userApiToken, "");

                var retorno = await client.ExecuteAsync<List<InviteResponseViewModel>>(request).ConfigureAwait(false);

                return retorno.Data;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<InviteResponseViewModel> ResendInviteAsync(string accountId, string inviteId, string userApiToken)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/{accountId}/user_invites/{inviteId}/resend")
                {
                    Authenticator = new HttpBasicAuthenticator(userApiToken, "")
                };

                var request = new RestRequest(Method.POST);

                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<InviteResponseViewModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new InviteResponseViewModel();

                var errors = retorno.IuguMapErrors<InviteResponseViewModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<InviteResponseViewModel> CancelInviteAsync(string accountId, string userApiToken, string inviteId)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/{accountId}/user_invites/{inviteId}/cancel")
                {
                    Authenticator = new HttpBasicAuthenticator(userApiToken, "")
                };

                var request = new RestRequest(Method.PUT);

                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<InviteResponseViewModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new InviteResponseViewModel();

                var errors = retorno.IuguMapErrors<InviteResponseViewModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguInvoiceResponseMessage> SendDuplicateAsync(string invoiceId, DuplicateViewModel model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/invoices/{invoiceId}/duplicate")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguInvoiceResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguInvoiceResponseMessage();

                var errors = retorno.IuguMapErrors<IuguInvoiceResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguInvoiceResponseMessage> ResendInvoiceEmailAsync(string invoiceId, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/invoices/{invoiceId}/send_email")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                request.AddHeader("content-type", "application/json");

                var retorno = await client.ExecuteAsync<IuguInvoiceResponseMessage>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguInvoiceResponseMessage();

                var errors = retorno.IuguMapErrors<IuguInvoiceResponseMessage>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);


                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguValidadeRequestPaymentResponseViewModel> ValidatePaymentRequestAsync(ValidateRequestPayment model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/payment_requests/validade")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                model.Detailed = true;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguValidadeRequestPaymentResponseViewModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguValidadeRequestPaymentResponseViewModel();

                var errors = retorno.IuguMapErrors<IuguValidadeRequestPaymentResponseViewModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguPaymentBookletResponse> CreatePaymentBookletAsync(IuguPaymentBookletsRequest model, string apiToken = null)
        {
            try
            {
                var client = new RestClient($"{BaseUrl}/payment_booklets")
                {
                    Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "")
                };

                var request = new RestRequest(Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguPaymentBookletResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguPaymentBookletResponse();

                var errors = retorno.IuguMapErrors<IuguPaymentBookletResponse>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorre um erro", ex);
            }
        }

        public async Task<IuguPaymentBookletResponse> GetPaymentBookletById(string id, string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/payment_booklets/{id}";

                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<IuguPaymentBookletResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguPaymentBookletResponse();

                var errors = retorno.IuguMapErrors<IuguPaymentBookletResponse>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<PaymentBookletsModel> GetPaymentBooklets(string apiToken = null)
        {
            try
            {
                var url = $"{BaseUrl}/payment_booklets";

                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                client.Authenticator = new HttpBasicAuthenticator(apiToken ?? _iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<PaymentBookletsModel>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new PaymentBookletsModel();

                var errors = retorno.IuguMapErrors<PaymentBookletsModel>(_iuguCredentials.ShowContent);

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguMarketPlaceConfigResponse> GetCredentialsSubAccounts(string subAccountId = null)
        {
            try
            {
                var url = $"{BaseUrl}/retrieve_subaccounts_api_token";

                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                client.Authenticator = new HttpBasicAuthenticator(_iuguCredentials.KeyUsage, "");

                var retorno = await client.ExecuteAsync<IuguMarketPlaceConfigResponse>(request).ConfigureAwait(false);

                var _return = retorno.Data ?? new IuguMarketPlaceConfigResponse();

                var errors = retorno.IuguMapErrors<IuguMarketPlaceConfigResponse>(_iuguCredentials.ShowContent);

                if (!string.IsNullOrEmpty(subAccountId))
                {
                    _return.Accounts = _return.Accounts.Where(x => x.Key == subAccountId).ToDictionary(i => i.Key, i => i.Value);
                }

                _return.Error = errors.Error;
                _return.MessageError = errors.MessageError;
                _return.HasError = errors.HasError;
                _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }


        public async Task<IuguAccountCreateResponseModel> GetSubAccountByDocument(DataBankViewModel model)
        {
            IuguAccountCreateResponseModel response = new();
            IuguMarketPlaceResponse iuguMarketPlaces = null;

            if (!string.IsNullOrEmpty(model.Cpf))
            {
                iuguMarketPlaces = await GetAccountsAsync(new() { Query = model.Cpf?.OnlyNumbers() });
            }

            if ((iuguMarketPlaces == null || iuguMarketPlaces.HasError) && !string.IsNullOrEmpty(model.Cnpj))
            {
                iuguMarketPlaces = await GetAccountsAsync(new() { Query = model.Cnpj?.OnlyNumbers() });
            }

            var iuguSubAccount = iuguMarketPlaces.Items.FirstOrDefault();

            response.HasError = iuguMarketPlaces.TotalItems == 0;

            if (iuguSubAccount != null)
            {
                response.AccountId = iuguSubAccount.Id;
                response.Name = iuguSubAccount.Name;

                var iuguSubAccountCredentials = await GetCredentialsSubAccounts(response.AccountId);

                if (iuguSubAccountCredentials.HasError == false && iuguSubAccountCredentials.Accounts.Count == 1)
                {
                    var marketPlaceSettings = iuguSubAccountCredentials.Accounts.GetOrDefault(response.AccountId);

                    response.LiveApiToken = marketPlaceSettings?.LiveToken;
                    response.TestApiToken = marketPlaceSettings?.TestToken;
                    response.UserToken = marketPlaceSettings?.UserToken;
                }
            }

            return response;
        }


        public async Task<IuguTransferModel> RepasseValoresAsync(string apiTokenSubConta, string accountId, decimal valorDecimal, string apiToken = null, bool toWithdraw = true)
        {
            try
            {
                apiToken ??= _iuguCredentials.LiveKeyRSA;

                var model = new IuguTrasferValuesModel()
                {
                    AmoutCents = Convert.ToInt32($"{valorDecimal * 100:0}"),
                    Receive = accountId,
                    ApiToken = _iuguCredentials.LiveKeyRSA
                };

                var _return = await SendRequestAsync<IuguTransferModel>(new()
                {
                    Url = $"{BaseUrl}/transfers",
                    Data = model,
                    Method = Method.POST,
                    ApiToken = apiToken,
                    NeedSignature = true
                });

                #region SOLICITAR SAQUE

                if (_return.HasError == false && valorDecimal >= 5 && toWithdraw)
                {
                    new Task(async () => { await SolicitarSaqueAsync(accountId, valorDecimal, apiTokenSubConta); }).Start();
                }

                #endregion SOLICITAR SAQUE

                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguWithdrawalModel> SolicitarSaqueAsync(string accountId, decimal valorSaque, string apiToken = null)
        {
            try
            {
                apiToken ??= _iuguCredentials.LiveKeyRSA;

                var client = new RestClient($"{BaseUrl}/accounts/{accountId}/request_withdraw?api_token={apiToken}");

                var request = new RestRequest(Method.POST);

                var model = new
                {
                    amount = valorSaque.ToString(CultureInfo.InvariantCulture),
                    api_token = _iuguCredentials.LiveKeyRSA
                };

                var json = JsonConvert.SerializeObject(model);

                SetSignatureRSA(client, request, json, apiToken);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var retorno = await client.ExecuteAsync<IuguWithdrawalResponse>(request).ConfigureAwait(false);

                var errors = retorno.IuguMapErrors<IuguWithdrawalResponse>(_iuguCredentials.ShowContent);
                var _return = new IuguWithdrawalModel { Error = errors.Error, MessageError = errors.MessageError, HasError = errors.HasError };

                if (retorno.Data != null)
                {
                    return new IuguWithdrawalModel()
                    {
                        Agencia = retorno.Data.BankAddress?.BankAg,
                        Banco = retorno.Data.BankAddress?.Bank,
                        Conta = retorno.Data.BankAddress?.BankCc,
                        AccountId = retorno.Data?.AccountId,
                        WithdrawalId = retorno.Data?.Id,
                        Valor = retorno.Data?.Amount,
                        Type = retorno.Data?.BankAddress?.AccountType
                    };
                }
                return _return;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro", ex);
            }
        }

        public async Task<IuguRequestPaymentResponseViewModel> CreatePaymentRequestAsync(IuguRequestPaymentViewModel model, string apiToken = null)
        {
            var resultValidate = await ValidatePaymentRequestAsync(new() { Barcode = model.Barcode }, apiToken ?? _iuguCredentials.LiveKey);

            if (resultValidate.HasError)
            {
                return new IuguRequestPaymentResponseViewModel()
                {
                    HasError = resultValidate.HasError,
                    Error = resultValidate.Error,
                    Content = resultValidate.Content,
                    MessageError = resultValidate.MessageError ?? resultValidate.Message
                };
            }
            model.ApiToken = _iuguCredentials.LiveKeyRSA;
            apiToken ??= _iuguCredentials.LiveKeyRSA;

            return await SendRequestAsync<IuguRequestPaymentResponseViewModel>(new()
            {
                Url = $"{BaseUrl}/payment_requests",
                Method = Method.POST,
                Data = model,
                ApiToken = apiToken,
                NeedSignature = true
            });
        }

        public async Task<ValidadeSignatureResponseViewModel> ValidateSignature(object model)
        {
            return await SendRequestAsync<ValidadeSignatureResponseViewModel>(new()
            {
                Url = $"{BaseUrl}/signature/validate",
                Method = Method.POST,
                Data = model,
                ApiToken = _iuguCredentials.LiveKeyRSA,
                NeedSignature = true
            });
        }
        private static string GetRequestTime() => DateTimeOffset.Now.ToString("yyyy-MM-dd'T'HH:mm:sszzz");

        private RSA GetPrivateKey()
        {
            if (string.IsNullOrEmpty(_iuguCredentials.PrivateKeyPath))
                throw new ArgumentNullException("PrivateKeyPath");

            var path = Path.Combine(Directory.GetCurrentDirectory(), _iuguCredentials.PrivateKeyPath.TrimStart('/'));

            if (File.Exists(path) == false)
                throw new FileNotFoundException($"arquivo {path} não encontrado, coloque o arquivo no caminho informado ou declare o caminho no campo \"IUGU.PrivateKeyPath\". O mesmo é obrigatório para assinar a request");

            var keyText = File.ReadAllText(path);
            var keyBase64 = keyText
                .Replace("-----BEGIN PRIVATE KEY-----", "")
                .Replace("-----END PRIVATE KEY-----", "")
                .Replace("\n", "")
                .Replace("\r", "");

            var keyBytes = Convert.FromBase64String(keyBase64);
            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(keyBytes, out _);
            return rsa;
        }

        private void SetSignatureRSA(RestClient client, RestRequest request, string json, string apiToken)
        {
            if (string.IsNullOrEmpty(apiToken))
                throw new ArgumentNullException("Infome o campo IUGU.LiveKeyRSA no appsettigns.{environment}.json ou no Settings/Config.json");

            var privateKeyRSA = GetPrivateKey();
            var requestTime = GetRequestTime();
            var signature = SignBody(request.Method.ToString().ToUpper(), client.BaseUrl.AbsolutePath, apiToken, requestTime, json, privateKeyRSA);

            request.AddHeader("Request-Time", requestTime)
                   .AddHeader("Signature", $"signature={signature}");
        }

        private static string SignBody(string method, string endpoint, string apiToken, string requestTime, string body, RSA rsa)
        {
            var pattern = $"{method}|{endpoint}\n{apiToken}|{requestTime}\n{body}";
            var bytesToSign = Encoding.UTF8.GetBytes(pattern);

            byte[] signatureBytes = rsa.SignData(bytesToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signatureBytes);
        }

        private async Task<T> SendRequestAsync<T>(SendRequest sendRequest) where T : IuguBaseErrors
        {
            sendRequest.ApiToken ??= _iuguCredentials.LiveKeyRSA;

            var client = new RestClient($"{sendRequest.Url}?api_token={sendRequest.ApiToken}");

            var request = new RestRequest(sendRequest.Method);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            if (sendRequest.Data != null)
            {
                var json = JsonConvert.SerializeObject(sendRequest.Data, Formatting.None);

                if (sendRequest.NeedSignature)
                    SetSignatureRSA(client, request, json, sendRequest.ApiToken);

                request.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            var retorno = await client.ExecuteAsync<T>(request).ConfigureAwait(false);

            var _return = retorno.Data ?? Activator.CreateInstance<T>();

            var errors = retorno.IuguMapErrors<T>(_iuguCredentials.ShowContent);

            _return.Error = errors.Error;
            _return.MessageError = errors.MessageError;
            _return.HasError = errors.HasError;
            _return.Content = retorno.ShowContent(_iuguCredentials.ShowContent);

            return _return;
        }

    }

}