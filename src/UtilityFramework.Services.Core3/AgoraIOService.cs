using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Core3.Interface;
using UtilityFramework.Services.Core3.Models.AgoraIO;
using UtilityFramework.Services.Core3.Models.AgoraIO.Media;

namespace UtilityFramework.Services.Core3
{
    public class AgoraIOService : IAgoraIOService
    {
        private readonly SettingsViewModel _agoraIOSettings;
        private readonly string _baseUrl = "https://api.agora.io/v1/apps";


        public AgoraIOService()
        {
            var setttings = Utilities.GetConfigurationRoot().GetSection("AgoraIO").Get<SettingsViewModel>();

            if (setttings == null)
                throw new Exception("Informe as credênciais na propriedade \"AgoraIO\"  no arquivo ./Settings/Config.json");

            if (string.IsNullOrEmpty(setttings.AppID))
                throw new Exception("AppId Agora.io não informado, verifique o arquivo config.json");

            if (string.IsNullOrEmpty(setttings.AppCertificate))
                throw new Exception("AppCertificate Agora.io não informado, verifique o arquivo config.json");

            _agoraIOSettings = setttings;
        }


        /// <summary>
        /// GERAR TOKEN DE ACESSO AGORAIO
        /// </summary>
        /// <param name="channnelId"></param>
        /// <param name="uid"></param>
        /// <param name="expiredTs"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        public string GenerateToken(string channnelId, string uid, uint expiredTs, List<Privileges> privileges)
        {
            try
            {
                var tokenBuilder = new AccessToken(_agoraIOSettings.AppID, _agoraIOSettings.AppCertificate, channnelId, uid);

                if (privileges != null && privileges.Count > 0)
                {
                    for (int i = 0; i < privileges.Count; i++)
                        tokenBuilder.addPrivilege(privileges[i], expiredTs);
                }

                return tokenBuilder.build();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GERAR TOKEN DE ACESSO AGORAIO
        /// </summary>
        /// <param name="channnelId"></param>
        /// <param name="uid"></param>
        /// <param name="ts"></param>
        /// <param name="salt"></param>
        /// <param name="expiredTs"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        public string GenerateToken(string channnelId, string uid, uint ts, uint salt, uint expiredTs, List<Privileges> privileges)
        {
            try
            {
                var tokenBuilder = new AccessToken(_agoraIOSettings.AppID, _agoraIOSettings.AppCertificate, channnelId, uid, ts, salt);

                if (privileges != null && privileges.Count > 0)
                {
                    for (int i = 0; i < privileges.Count; i++)
                        tokenBuilder.addPrivilege(privileges[i], expiredTs);
                }

                return tokenBuilder.build();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AcquireResponseViewModel> AcquireResourceId(AcquireRequestViewModel model)
        {
            try
            {
                var client = new RestClient(_baseUrl);

                client.Authenticator = new HttpBasicAuthenticator(_agoraIOSettings.UserName, _agoraIOSettings.Password);

                var request = new RestRequest($"/{_agoraIOSettings.AppID}/cloud_recording/acquire", Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync<AcquireResponseViewModel>(request);

                return MapResponse(response);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<AcquireResponseViewModel> StartRecording(string resourceId, AcquireRequestViewModel model)
        {
            try
            {
                var client = new RestClient(_baseUrl);

                client.Authenticator = new HttpBasicAuthenticator(_agoraIOSettings.UserName, _agoraIOSettings.Password);

                var request = new RestRequest($"/{_agoraIOSettings.AppID}/cloud_recording/resourceid/{resourceId}/mode/mix/start", Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync<AcquireResponseViewModel>(request);

                return MapResponse(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<StopResponseViewModel> Query(string resourceId, string sid)
        {
            var client = new RestClient(_baseUrl);

            client.Authenticator = new HttpBasicAuthenticator(_agoraIOSettings.UserName, _agoraIOSettings.Password);

            var request = new RestRequest($"/{_agoraIOSettings.AppID}/cloud_recording/resourceid/{resourceId}/sid/{sid}/mode/mix/query", Method.GET);

            request.AddHeader("content-type", "application/json");

            var response = await client.ExecuteAsync<StopResponseViewModel>(request);

            return MapResponse(response);

        }

        public async Task<StopResponseViewModel> StopRecording(string resourceId, string sid, StopRequestViewModel model)
        {
            try
            {
                var client = new RestClient(_baseUrl);

                client.Authenticator = new HttpBasicAuthenticator(_agoraIOSettings.UserName, _agoraIOSettings.Password);

                var request = new RestRequest($"/{_agoraIOSettings.AppID}/cloud_recording/resourceid/{resourceId}/sid/{sid}/mode/mix/stop", Method.POST);

                var json = JsonConvert.SerializeObject(model);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                var response = await client.ExecuteAsync<StopResponseViewModel>(request);

                return MapResponse(response);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private TDestination MapResponse<TDestination>(IRestResponse<TDestination> requestResponse) where TDestination : BaseResponseViewModel
        {
            try
            {
                if (requestResponse.Data == null)
                    throw new Exception();

                if (_agoraIOSettings.ShowContent)
                    requestResponse.Data.Content = requestResponse.Content;

                requestResponse.Data.CheckHasError(requestResponse.StatusCode);
                return requestResponse.Data;

            }
            catch (Exception)
            {
                var data = Activator.CreateInstance<TDestination>();

                if (_agoraIOSettings.ShowContent)
                    data.Content = requestResponse.Content;

                data.Code = 99999;
                data.Reason = "Erro desconhecido, verifique o content para mais informações";
                data.CheckHasError(requestResponse.StatusCode);

                return data;
            }
        }
    }
}