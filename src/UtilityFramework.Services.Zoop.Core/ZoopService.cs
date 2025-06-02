using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using UtilityFramework.Application.Core;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.Interface;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core
{
    public class ZoopService : IZoopCreditCardService, IZoopBuyerService, IZoopTransactionService, IZoopSellerService, IZoopBankAccountService, IZoopWebHookService
    {
        private readonly string _marketPlaceId;
        private readonly string _authKey;
        private readonly string _masterSeller;
        private readonly bool _isSandBox;
        private readonly RestClient _restClient;
        private Dictionary<string, string> tags = new Dictionary<string, string>();

        public ZoopService()
        {
            var zoopSettings = UtilityFramework.Application.Core.Utilities.GetConfigurationRoot().GetSection("zoopSettings").Get<ZoopSettings>();

            if (zoopSettings == null)
                throw new Exception("zoopSettings inválido, verifique os dados informados no arquivo Config.json");

            var zoopConfiguration = zoopSettings.SandBox ? zoopSettings.Dev : zoopSettings.Prod;

            if (zoopConfiguration == null)
                throw new Exception("zoopSettings inválido, verifique os dados informados no arquivo Config.json");

            _isSandBox = zoopSettings?.SandBox ?? false;
            _marketPlaceId = zoopConfiguration?.MarketPlaceId;
            _authKey = zoopConfiguration?.Key;
            _masterSeller = zoopConfiguration?.MasterSellerId;
            _restClient = new RestClient(ZoopMethods.BaseUrl);
            _restClient.Authenticator = new HttpBasicAuthenticator(_authKey, null);
        }

        public CardViewModel AssociateCreditCard(AssociateViewModel model, string marketPlaceId = null)
        {

            var response = new CardViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.AssociateCreaditCard.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);


                var result = _restClient.Execute<CardViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<CardViewModel> AssociateCreditCardAsync(AssociateViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new CardViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.AssociateCreaditCard.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);


                var result = await _restClient.ExecuteAsync<CardViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);

            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> DeleteBuyer(string buyerId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{buyer_id}}", buyerId);

                var request = new RestRequest(ZoopMethods.DeleteBuyer.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> DeleteBuyerAsync(string buyerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                tags.Add("{{buyer_id}}", marketPlaceId);

                var request = new RestRequest(ZoopMethods.DeleteBuyer.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> DeleteCard(string card_id, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{card_id}}", card_id);

                var request = new RestRequest(ZoopMethods.DeleteCredidCard.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> DeleteCardAsync(string card_id, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{card_id}}", card_id);

                var request = new RestRequest(ZoopMethods.DeleteCredidCard.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> DeleteSeller(string sellerId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{id}}", sellerId);

                var request = new RestRequest(ZoopMethods.DeleteSeller.ReplaceTag(tags), Method.POST);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> DeleteSellerAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{id}}", sellerId);

                var request = new RestRequest(ZoopMethods.DeleteSeller.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GenerateTokenCard(RegisterCreditCardViewModel model, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {


                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateCardToken.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> GenerateTokenCardAsync(RegisterCreditCardViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateCardToken.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BuyerViewModel GetBuyerById(string buyerId, string marketPlaceId = null)
        {
            var response = new BuyerViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{buyer_id}}", buyerId);

                var request = new RestRequest(ZoopMethods.GetBuyerById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BuyerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BuyerViewModel> GetBuyerByIdAsync(string buyerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BuyerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{buyer_id}}", buyerId);

                var request = new RestRequest(ZoopMethods.GetBuyerById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BuyerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public CardViewModel GetCard(string credit_id, string marketPlaceId = null)
        {
            var response = new CardViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{card_id}}", credit_id);

                var request = new RestRequest(ZoopMethods.GetCreditCardById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<CardViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<CardViewModel> GetCardAsync(string card_id, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new CardViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{card_id}}", card_id);

                var request = new RestRequest(ZoopMethods.GetCreditCardById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<CardViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GetTokenCard(string token_id, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                if (string.IsNullOrEmpty(token_id))
                {
                    response.HasError = true;
                    response.MessageErro = "Informe o token";
                }
                else
                {
                    marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                    tags.Add("{{token_id}}", token_id);

                    var request = new RestRequest(ZoopMethods.GetInfoToken.ReplaceTag(tags), Method.GET);

                    request.AddHeader("content-type", "application/json");

                    var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                    response = result?.Data;
                    response?.CheckHasError(result?.Content);
                }
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> GetTokenCardAsync(string token_id, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                if (string.IsNullOrEmpty(token_id))
                {
                    response.HasError = true;
                    response.MessageErro = "Informe o token";
                }
                else
                {
                    marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                    tags.Add("{{token_id}}", token_id);

                    var request = new RestRequest(ZoopMethods.GetInfoToken.ReplaceTag(tags), Method.GET);

                    request.AddHeader("content-type", "application/json");

                    var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                    response = result?.Data;

                    response?.CheckHasError(result?.Content);
                }
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<BuyerViewModel> ListBuyerByMarketPlaceId(ZoopPaginationViewModel pagination = null, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<BuyerViewModel>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListBuyerByMarketPlace.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = _restClient.Execute<BaseResponseViewModel<BuyerViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);

            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<BuyerViewModel>> ListBuyerByMarketPlaceIdAsync(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<BuyerViewModel>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListBuyerByMarketPlace.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<BuyerViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<ItemMerchantCategoryCodeViewModel> ListMCC(ZoopPaginationViewModel pagination = null)
        {
            var response = new BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>();
            try
            {

                var request = new RestRequest(ZoopMethods.ListMerchantCategoryCode.AddQueryStringParameters(pagination), Method.GET);

                var result = _restClient.Execute<BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }
        public async Task<BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>> ListMCCAsync(ZoopPaginationViewModel pagination = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>();
            try
            {

                var request = new RestRequest(ZoopMethods.ListMerchantCategoryCode.AddQueryStringParameters(pagination), Method.GET);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<ItemMerchantCategoryCodeViewModel>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BuyerViewModel RegisterBuyer(BuyerViewModel model, string marketPlaceId = null)
        {
            var response = new BuyerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterBuyer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BuyerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BuyerViewModel> RegisterBuyerAsync(BuyerViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BuyerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterBuyer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BuyerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> ReverseTransaction(ReverseTransactionViewModel model, string transactionId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{transaction_id}}", transactionId);

                var request = new RestRequest(ZoopMethods.ReverseTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> ReverseTransactionAsync(ReverseTransactionViewModel model, string transactionId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{transaction_id}}", transactionId);


                var request = new RestRequest(ZoopMethods.ReverseTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseTransactionViewModel Transaction(TransactionViewModel model, string marketPlaceId = null)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                if (string.IsNullOrEmpty(model?.OnBehalfOf))
                    model.OnBehalfOf = _masterSeller;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<ResponseTransactionViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseTransactionViewModel> TransactionAsync(TransactionViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseTransactionViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);


                if (string.IsNullOrEmpty(model?.OnBehalfOf))
                    model.OnBehalfOf = _masterSeller;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<ResponseTransactionViewModel>(request).ConfigureAwait(configureAwait);



                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }
            return response;
        }

        public SellerViewModel GetSellerById(string sellerId, string marketPlaceId = null)
        {
            var response = new SellerViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{id}}", sellerId);

                var request = new RestRequest(ZoopMethods.GetSellerById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<SellerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<SellerViewModel> GetSellerByIdAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new SellerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{id}}", sellerId);

                var request = new RestRequest(ZoopMethods.GetSellerById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<SellerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
                var customResponse = response;
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<SellerViewModel> ListSellers<SellerViewModel>(ZoopPaginationViewModel pagination = null, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<SellerViewModel>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListSellers.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<SellerViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
                var customResponse = response;

            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<SellerViewModel>> ListSellersAsync<SellerViewModel>(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<SellerViewModel>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListSellers.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<SellerViewModel>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public SellerViewModel RegisterSellerBusiness(SellerViewModel model, string marketPlaceId = null)
        {
            var response = new SellerViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterSellerBusiness.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<SellerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<SellerViewModel> RegisterSellerBusinessAsync(SellerViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new SellerViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterSellerBusiness.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<SellerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public SellerViewModel RegisterSellerIndividual(SellerViewModel model, string marketPlaceId = null)
        {
            var response = new SellerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterSellerIndividual.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<SellerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<SellerViewModel> RegisterSellerIndividualAsync(SellerViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new SellerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterSellerIndividual.ReplaceTag(tags), Method.POST);

                model.Type = TypeSeller.Individual;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<SellerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<CardViewModel> ListCard(ZoopPaginationViewModel pagination = null, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<CardViewModel>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListCard.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = _restClient.Execute<BaseResponseViewModel<CardViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<CardViewModel>> ListCardAsync(ZoopPaginationViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<CardViewModel>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.ListCard.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<CardViewModel>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseTransactionViewModel TransactionWithTokenCard(RegisterTokenTransactionViewModel model, string marketPlaceId = null)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                if (string.IsNullOrEmpty(model?.OnBehalfOf))
                    model.OnBehalfOf = _masterSeller;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<ResponseTransactionViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseTransactionViewModel> TransactionWithTokenCardAsync(RegisterTokenTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                if (string.IsNullOrEmpty(model?.OnBehalfOf))
                    model.OnBehalfOf = _masterSeller;

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<ResponseTransactionViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BuyerViewModel> GetBuyerByCpfOrCnpjAsync(string cpfOrCnpj, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BuyerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{cpfOrCnpj}}", cpfOrCnpj);

                var request = new RestRequest(ZoopMethods.GetBuyerByCpfOrCnpj.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BuyerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BuyerViewModel GetBuyerByCpfOrCnpj(string cpfOrCnpj, string marketPlaceId = null)
        {
            var response = new BuyerViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{cpfOrCnpj}}", cpfOrCnpj);

                var request = new RestRequest(ZoopMethods.GetBuyerByCpfOrCnpj.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BuyerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseTransactionViewModel TransactionWithCreditCard(RegisterCreditCardTransactionViewModel model, string marketPlaceId = null)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<ResponseTransactionViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseTransactionViewModel TransactionWithBankSlip(BankSlipTransactionViewModel model, string marketPlaceId = null)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<ResponseTransactionViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponsePaymentMethodViewModel GetBankSlip(string banksplip_id, string marketPlaceId = null)
        {
            var response = new ResponsePaymentMethodViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{boleto_id}}", banksplip_id);


                var request = new RestRequest(ZoopMethods.GetBankSlip.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<ResponsePaymentMethodViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponsePaymentMethodViewModel> GetBankSlipAsync(string banksplip_id, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponsePaymentMethodViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{boleto_id}}", banksplip_id);


                var request = new RestRequest(ZoopMethods.GetBankSlip.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<ResponsePaymentMethodViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseTransactionViewModel> TransactionWithBankSlipAsync(BankSlipTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<ResponseTransactionViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseTransactionViewModel> TransactionWithCreditCardAsync(RegisterCreditCardTransactionViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseTransactionViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateTransaction.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<ResponseTransactionViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GetTokenBank(string token_id, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                if (string.IsNullOrEmpty(token_id))
                {
                    response.HasError = true;
                    response.MessageErro = "Informe o token";
                }
                else
                {
                    marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                    tags.Add("{{token_id}}", token_id);

                    var request = new RestRequest(ZoopMethods.GetInfoToken.ReplaceTag(tags), Method.GET);

                    request.AddHeader("content-type", "application/json");

                    var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                    response = result?.Data;
                    response?.CheckHasError(result?.Content);
                }
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }
        public async Task<BaseResponseViewModel<object>> GetTokenBankAsync(string token_id, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                if (string.IsNullOrEmpty(token_id))
                {
                    response.HasError = true;
                    response.MessageErro = "Informe o token";
                }
                else
                {
                    marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                    tags.Add("{{token_id}}", token_id);

                    var request = new RestRequest(ZoopMethods.GetInfoToken.ReplaceTag(tags), Method.GET);

                    request.AddHeader("content-type", "application/json");

                    var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                    response = result?.Data;

                    response?.CheckHasError(result?.Content);
                }
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> RegisterTokenBank(RegisterTokenBankAccountViewModel model, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateBankToken.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> RegisterTokenBankAsync(RegisterTokenBankAccountViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.CreateBankToken.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> DeleteBank(string bankId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> DeleteBankAsync(string bankId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> AssociateTokenBankWithCustomer(AssociateViewModel model, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.AssociateBankAccount.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> AssociateTokenBankWithCustomerAsync(AssociateViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.AssociateBankAccount.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseWebHookViewModel RegisterWebHook(RegisterWebHookViewModel model, string marketPlaceId = null)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterWebHook.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<ResponseWebHookViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseWebHookViewModel> RegisterWebHookAsync(RegisterWebHookViewModel model, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterWebHook.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<ResponseWebHookViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseWebHookViewModel GetWebHookById(string webhookId, string marketPlaceId = null)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<ResponseWebHookViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseWebHookViewModel> GetWebHookByIdAsync(string webhookId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<ResponseWebHookViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public ResponseWebHookViewModel DeleteWebHook(string webhookId, string marketPlaceId = null)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<ResponseWebHookViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseWebHookViewModel> DeleteWebHookAsync(string webhookId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseWebHookViewModel();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.WebHookById.ReplaceTag(tags), Method.DELETE);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<ResponseWebHookViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<ResponseWebHookViewModel> ListWebHook(ZoopPaginationViewModel pagination = null, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<ResponseWebHookViewModel>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterWebHook.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = _restClient.Execute<BaseResponseViewModel<ResponseWebHookViewModel>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<ResponseWebHookViewModel>> ListWebHookAsync(RegisterWebHookViewModel pagination = null, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<ResponseWebHookViewModel>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.RegisterWebHook.ReplaceTag(tags).AddQueryStringParameters(pagination), Method.GET);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<ResponseWebHookViewModel>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GetBanksBySeller(string sellerId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);

                var request = new RestRequest(ZoopMethods.GetBankBySellerId.ReplaceTag(tags), Method.POST);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> GetBanksBySellerAsync(string sellerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);

                var request = new RestRequest(ZoopMethods.GetBankBySellerId.ReplaceTag(tags), Method.POST);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GetBankInformation(string bankId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetBankById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> GetBankInformationAsync(string bankId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetBankById.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> GetBanksByMarketPlace(string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetBankAccounts.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> GetBanksByMarketPlaceAsync(string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetBankAccounts.ReplaceTag(tags), Method.GET);

                request.AddHeader("content-type", "application/json");

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> RegisterDocument(IFormFile file, RegisterDocumentViewModel model, string sellerId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                if (string.IsNullOrEmpty(sellerId))
                    throw new Exception("Informe o identificador do vendedor");

                if (Equals(file, null))
                    throw new Exception("Por favor inclua um arquivo");

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);

                var request = new RestRequest(ZoopMethods.RegisterDocuments.ReplaceTag(tags), Method.POST);

                request.MapRequestParameter(model);


                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    request.AddFile("file", fileBytes, file.FileName);
                }

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<ResponseUploadViewModel> RegisterDocumentAsync(IFormFile file, RegisterDocumentViewModel model, string sellerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new ResponseUploadViewModel();
            try
            {
                if (string.IsNullOrEmpty(sellerId))
                    throw new Exception("Informe o identificador do vendedor");

                if (Equals(file, null))
                    throw new Exception("Por favor inclua um arquivo");

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);

                var request = new RestRequest(ZoopMethods.RegisterDocuments.ReplaceTag(tags), Method.POST);

                request.MapRequestParameter(model);

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    request.AddFile("file", fileBytes, file.FileName);
                }

                var result = await _restClient.ExecuteAsync<ResponseUploadViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public SellerViewModel GetSellerByCpfOrCnpj(FilterSellerViewModel filter, string marketPlaceId = null)
        {
            var response = new SellerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetSellerByCpfOrCnpj.ReplaceTag(tags).AddQueryStringParameters(filter), Method.GET);

                var result = _restClient.Execute<SellerViewModel>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<SellerViewModel> GetSellerByCpfOrCnpjAsync(FilterSellerViewModel filter, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new SellerViewModel();
            try
            {
                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);

                var request = new RestRequest(ZoopMethods.GetSellerByCpfOrCnpj.ReplaceTag(tags).AddQueryStringParameters(filter), Method.GET);

                var result = await _restClient.ExecuteAsync<SellerViewModel>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> ChangeAutomaticTransfer(ChangeAutomaticTransferViewModel model, string sellerId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);
                var request = new RestRequest(ZoopMethods.ChangeAutomaticTransfer.ReplaceTag(tags), Method.POST);


                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> ChangeAutomaticTransferAsync(ChangeAutomaticTransferViewModel model, string sellerId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{seller_id}}", sellerId);
                var request = new RestRequest(ZoopMethods.ChangeAutomaticTransfer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> RequestTransfer(RequestTransferViewModel model, string bankId, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{bank_account_id}}", bankId);


                var request = new RestRequest(ZoopMethods.RequestTransfer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> RequestTransferAsync(RequestTransferViewModel model, string bankId, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{bank_account_id}}", bankId);

                var request = new RestRequest(ZoopMethods.RequestTransfer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public BaseResponseViewModel<object> RequestInternalTransfer(RequestTransferViewModel model, string sellerSender, string sellerReceive, string marketPlaceId = null)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{owner}}", sellerSender);
                tags.Add("{{receiver}}", sellerReceive);

                var request = new RestRequest(ZoopMethods.RequestInternalTransfer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = _restClient.Execute<BaseResponseViewModel<object>>(request);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }

        public async Task<BaseResponseViewModel<object>> RequestInternalTransferAsync(RequestTransferViewModel model, string sellerSender, string sellerReceive, string marketPlaceId = null, bool configureAwait = false)
        {
            var response = new BaseResponseViewModel<object>();
            try
            {

                marketPlaceId.ChangeMarketPlace(tags, _marketPlaceId);
                tags.Add("{{owner}}", sellerSender);
                tags.Add("{{receiver}}", sellerReceive);

                var request = new RestRequest(ZoopMethods.RequestInternalTransfer.ReplaceTag(tags), Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var result = await _restClient.ExecuteAsync<BaseResponseViewModel<object>>(request).ConfigureAwait(configureAwait);

                response = result?.Data;

                response?.CheckHasError(result?.Content);
            }
            catch (Exception ex)
            {
                response.ReturnException(ex);
            }

            return response;
        }
    }
}