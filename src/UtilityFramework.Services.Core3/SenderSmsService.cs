using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Core3.Interface;
using UtilityFramework.Services.Core3.Models;

namespace UtilityFramework.Services.Core3
{
    public class SenderSmsService : ISenderSmsService
    {
        /// <summary>
        /// ENVIO DE SMS IAGENT
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>

        public SmsResponse SenderSmsIAgent(string phone, string message)
        {
            var smsCredentials =
                Utilities.GetConfigurationRoot().GetSection("iagente").Get<SmsCredentialsViewModel>();

            if (smsCredentials == null)
                throw new Exception("Informe as credenciais do serviço de sms.");

            var url = $"http://www.iagentesms.com.br/webservices/http.php?metodo=envio&usuario={smsCredentials.Username}&senha={smsCredentials.Password}&celular={phone.TrimSpaces()}&mensagem={message}";

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            var retorno = client.Execute<string>(request);

            return new SmsResponse()
            {
                Message = retorno.Data,
                Success = !string.IsNullOrEmpty(retorno.Content) && retorno.Content.Equals("OK")
            };
        }

        /// <summary>
        /// ENVIO DE SMS IAGENT
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="configureAwait"></param>
        /// <returns></returns>
        public async Task<SmsResponse> SenderSmsIAgentAsync(string phone, string message, bool configureAwait = false)
        {
            var smsCredentials =
                Utilities.GetConfigurationRoot().GetSection("iagente").Get<SmsCredentialsViewModel>();

            if (smsCredentials == null)
                throw new Exception("Informe as credenciais do serviço de sms.");

            var url = $"http://www.iagentesms.com.br/webservices/http.php?metodo=envio&usuario=fabiano@megaleios.com&senha=@Mega2017&celular={phone.TrimSpaces()}&mensagem={message}";

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            var retorno = await client.ExecuteAsync<string>(request).ConfigureAwait(configureAwait);

            return new SmsResponse()
            {
                Message = retorno.Data,
                Success = string.IsNullOrEmpty(retorno.Content) == false && retorno.Content.Equals("OK")
            };

        }

        /// <inheritdoc />
        /// <summary>
        /// ENVIO DE SMS INFOBIP
        /// </summary>
        /// <param name="from"></param>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="countryCode"></param>
        /// <param name="configureAwait"></param>
        /// <param name="removeZeroLeft"></param>
        /// <returns></returns>
        public async Task<SmsResponseViewModel> InfobipSenderSmsAsync(string from, string phone, string message, string countryCode = "55", bool configureAwait = false, bool removeZeroLeft = false)
        {

            try
            {
                if (removeZeroLeft)
                    phone = phone?.TrimStart('0');

                var client = new RestClient("https://api.infobip.com/sms/1/text/single");

                var infobipCredential =
                    Utilities.GetConfigurationRoot().GetSection("infobip").Get<SmsCredentialsViewModel>();

                client.Authenticator = new HttpBasicAuthenticator(infobipCredential.Username, infobipCredential.Password);

                var request = new RestRequest(Method.POST);

                var dataBody = new SmsRequestViewModel()
                {
                    From = from,
                    Text = message,
                    To = $"{countryCode}{phone.OnlyNumbers()}"
                };

                var json = JsonConvert.SerializeObject(dataBody);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                var response = await client.ExecuteAsync<SmsResponseViewModel>(request).ConfigureAwait(configureAwait);

                if (response?.Data != null)
                    response.Data.Erro = response.Data.Messages.Any(x => x.SmsStatusViewModel.GroupId == 5);

                return response == null ? new SmsResponseViewModel() : response.Data;

            }
            catch (Exception ex)
            {

                return new SmsResponseViewModel()
                {
                    Message = "Ocorreu um erro ao tentar enviar o sms",
                    MessageEx = $"{ex.InnerException} {ex.Message}".Trim(),
                    Erro = true
                };
            }

        }

        /// <inheritdoc />
        /// <summary>
        /// ENVIO DE SMS INFOBIT SINCRONO
        /// </summary>
        /// <param name="from">Nome do app</param>
        /// <param name="phone">Numero de telefone no formato DD#####-####</param>
        /// <param name="message"></param>
        /// <param name="countryCode">Codigo do país </param>
        /// <param name="removeZeroLeft"></param>
        /// <returns></returns>
        public SmsResponseViewModel InfobipSendSms(string from, string phone, string message, string countryCode = "55", bool removeZeroLeft = false)
        {

            try
            {
                if (removeZeroLeft)
                    phone = phone?.TrimStart('0');


                var client = new RestClient("https://api.infobip.com/sms/1/text/single");

                var infobipCredential =
                    Utilities.GetConfigurationRoot().GetSection("infobip").Get<SmsCredentialsViewModel>();

                if (infobipCredential == null)
                    throw new Exception("Informe as credenciais do serviço de sms.");

                client.Authenticator = new HttpBasicAuthenticator(infobipCredential.Username, infobipCredential.Password);

                var request = new RestRequest(Method.POST);

                var dataBody = new SmsRequestViewModel()
                {
                    From = from,
                    Text = message,
                    To = $"{countryCode}{phone.OnlyNumbers()}"
                };

                var json = JsonConvert.SerializeObject(dataBody);

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                var response = client.Execute<SmsResponseViewModel>(request);

                if (response?.Data != null)
                    response.Data.Erro = response.Data.Messages.Any(x => x.SmsStatusViewModel.GroupId == 5);

                return response == null ? new SmsResponseViewModel() : response.Data;

            }
            catch (Exception ex)
            {

                return new SmsResponseViewModel()
                {
                    Message = "Ocorreu um erro ao tentar enviar o sms",
                    MessageEx = $"{ex.InnerException} {ex.Message}".Trim(),
                    Erro = true
                };
            }

        }
        /// <summary>
        /// METODO DE ENVIO DE SMS PELO SERVIÇO SMSDEV
        /// </summary>
        /// <param name="message"></param>
        /// <param name="celPhone"></param>
        /// <param name="countryCode"></param>
        /// <param name="referenceId"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SmsDevViewModel> SmsDevSendSmsAsync(string message, string celPhone, string countryCode = "+55", string referenceId = null, DateTime? dateSend = null)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/send", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("type", 9, ParameterType.GetOrPost);
                request.AddParameter("number", $"{countryCode}{celPhone}".OnlyNumbers(), ParameterType.GetOrPost);
                request.AddParameter("msg", message.RemoveAccents(), ParameterType.GetOrPost);
                request.AddParameter("refer", referenceId, ParameterType.GetOrPost);

                if (dateSend != null)
                {
                    request.AddParameter("jobdate", dateSend.GetValueOrDefault().ToString("dd/MM/yyyy"), ParameterType.GetOrPost);
                    request.AddParameter("jobtime", dateSend.GetValueOrDefault().ToString("Hh:mm"), ParameterType.GetOrPost);

                }

                var response = await client.ExecuteAsync<SmsDevViewModel>(request).ConfigureAwait(false);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao tentar enviar sms para o numero {countryCode}{celPhone}", e);
            }
        }
        /// <summary>
        /// METODO DE ENVIO DE SMS PELO SERVIÇO SMS DEV
        /// </summary>
        /// <param name="message"></param>
        /// <param name="celPhone"></param>
        /// <param name="countryCode"></param>
        /// <param name="referenceId"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SmsDevViewModel SmsDevSendSms(string message, string celPhone, string countryCode = "+55", string referenceId = null, DateTime? dateSend = null)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/send", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("type", 9, ParameterType.GetOrPost);
                request.AddParameter("number", $"{countryCode}{celPhone}".OnlyNumbers(), ParameterType.GetOrPost);
                request.AddParameter("msg", message.RemoveAccents(), ParameterType.GetOrPost);
                request.AddParameter("refer", referenceId, ParameterType.GetOrPost);

                if (dateSend != null)
                {
                    request.AddParameter("jobdate", dateSend.GetValueOrDefault().ToString("dd/MM/yyyy"), ParameterType.GetOrPost);
                    request.AddParameter("jobtime", dateSend.GetValueOrDefault().ToString("Hh:mm"), ParameterType.GetOrPost);
                }

                var response = client.Execute<SmsDevViewModel>(request);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao tentar enviar sms para o numero {countryCode}{celPhone}", e);
            }

        }

        /// <summary>
        /// METODO PARA ENVIO DE SMS PARA VARIOS CELULAR COM SMSDEV
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<SmsDevViewModel>> SmsDevSendMultipleSmsAsync(List<SmsDevMultipleViewModel> data, DateTime? dateSend = null)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/multiple", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("type", 9, ParameterType.GetOrPost);

                for (int i = 1; i <= data.Count; i++)
                {
                    request.AddParameter($"number{i}", data[i].Number.OnlyNumbers(), ParameterType.GetOrPost);
                    request.AddParameter($"msg{i}", data[i].Message.RemoveAccents(), ParameterType.GetOrPost);
                    request.AddParameter($"refer{i}", data[i].Refer, ParameterType.GetOrPost);
                }


                if (dateSend != null)
                {
                    request.AddParameter("jobdate", dateSend.GetValueOrDefault().ToString("dd/MM/yyyy"), ParameterType.GetOrPost);
                    request.AddParameter("jobtime", dateSend.GetValueOrDefault().ToString("Hh:mm"), ParameterType.GetOrPost);

                }

                var response = await client.ExecuteAsync<List<SmsDevViewModel>>(request).ConfigureAwait(false);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao tentar enviar sms para varios numeros", e);
            }
        }
        /// <summary>
        /// METODO PARA ENVIO DE SMS PARA VARIOS CELULAR COM SMSDEV
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<SmsDevViewModel> SmsDevSendMultipleSms(List<SmsDevMultipleViewModel> data, DateTime? dateSend = null)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/multiple", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("type", 9, ParameterType.GetOrPost);

                for (int i = 1; i <= data.Count; i++)
                {
                    request.AddParameter($"number{i}", data[i].Number.OnlyNumbers(), ParameterType.GetOrPost);
                    request.AddParameter($"msg{i}", data[i].Message.RemoveAccents(), ParameterType.GetOrPost);
                    request.AddParameter($"refer{i}", data[i].Refer, ParameterType.GetOrPost);
                }


                if (dateSend != null)
                {
                    request.AddParameter("jobdate", dateSend.GetValueOrDefault().ToString("dd/MM/yyyy"), ParameterType.GetOrPost);
                    request.AddParameter("jobtime", dateSend.GetValueOrDefault().ToString("Hh:mm"), ParameterType.GetOrPost);

                }

                var response = client.Execute<List<SmsDevViewModel>>(request);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao tentar enviar sms para varios numeros", e);
            }
        }

        /// <summary>
        /// METODO PARA OBTER DETALHES DE UM SMS ENVIADO PELA SMSDEV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SmsDevViewModel> GetAsync(string id)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/get", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("action", "status", ParameterType.GetOrPost);
                request.AddParameter("id", id, ParameterType.GetOrPost);

                var response = await client.ExecuteAsync<SmsDevViewModel>(request).ConfigureAwait(false);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao obter o status do envio", e);

            }
        }

        /// <summary>
        /// METODO PARA OBTER DETALHES DE UM SMS ENVIADO PELA SMSDEV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SmsDevViewModel Get(string id)
        {
            try
            {
                var client = new RestClient("https://api.smsdev.com.br");
                var request = new RestRequest("/get", Method.GET);

                var key = Utilities.GetConfigurationRoot().GetSection("smsDev:apiKey").Get<string>();

                if (string.IsNullOrEmpty(key))
                    throw new Exception("Informe a key no arquivo Settings/Config.json smsDev:apiKey");

                request.AddParameter("key", key, ParameterType.GetOrPost);
                request.AddParameter("action", "status", ParameterType.GetOrPost);
                request.AddParameter("id", id, ParameterType.GetOrPost);

                var response = client.Execute<SmsDevViewModel>(request);

                return response.Data;
            }
            catch (Exception e)
            {

                throw new Exception($"Ocorreu um erro ao obter o status do envio", e);

            }
        }



    }
}