using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Services.Core3.Models;

namespace UtilityFramework.Services.Core3.Interface
{
    public interface ISenderSmsService
    {
        /// <summary>
        /// SEND SMS TO VALIDATE PHONE NUMBER
        /// </summary>
        /// <param name="phone">FORMAT 11999999999 OR 1199999999</param>
        /// <param name="message">MESSAGE, DONT USE SPECIAL CHARACTERS</param>
        /// <returns></returns>
        SmsResponse SenderSmsIAgent(string phone, string message);

        /// <summary>
        /// SEND SMS WITH INFOBIP
        /// </summary>
        /// <param name="from">APP NAME</param>
        /// <param name="phone">FORMAT 11999999999 OR 1199999999</param>
        /// <param name="message">MESSAGE, DONT USE SPECIAL CHARACTERS</param>
        /// <param name="countryCode">COUNTRY CODE EX: 55</param>
        /// <param name="removeZeroLeft"></param>
        /// <returns></returns>
        SmsResponseViewModel InfobipSendSms(string from, string phone, string message, string countryCode = "55", bool removeZeroLeft = false);

        /// <summary>
        /// SEND SMS TO VALIDATE PHONE NUMBER ASYNC
        /// </summary>
        /// <param name="phone">FORMAT 11999999999 OR 1199999999</param>
        /// <param name="message">MESSAGE, DONT USE SPECIAL CHARACTERS</param>
        /// <param name="configureAwait">USE CONTEXT</param>
        /// <returns></returns>
        Task<SmsResponse> SenderSmsIAgentAsync(string phone, string message, bool configureAwait = false);

        /// <summary>
        /// SEND SMS WITH INFOBIP
        /// </summary>
        /// <param name="from">APP NAME</param>
        /// <param name="phone">FORMAT 11999999999 OR 1199999999</param>
        /// <param name="message">MESSAGE, DONT USE SPECIAL CHARACTERS</param>
        /// <param name="countryCode">COUNTRY CODE EX: 55</param>
        /// <param name="configureAwait">USE CONTEXT</param>
        /// <param name="removeZeroLeft">REMOVER ZERO A ESQUERDA</param>
        /// <returns></returns>
        Task<SmsResponseViewModel> InfobipSenderSmsAsync(string from, string phone, string message, string countryCode = "55", bool configureAwait = false, bool removeZeroLeft = false);
        /// <summary>
        /// ENVIO DE SMS PARA UM CELULAR COM POSSIBILIDADE DE AGENDAMENTO
        /// </summary>
        /// <param name="message">MENSAGEM SEM CARACTERES ESPECIAIS</param>
        /// <param name="celPhone">NUMERO DO CELULAR</param>
        /// <param name="countryCode">55</param>
        /// <param name="referenceId">REFERENCIA</param>
        /// <param name="dateSend">DATA DE ENVIO PARA AGENDAMENTO</param>
        /// <returns></returns>
        Task<SmsDevViewModel> SmsDevSendSmsAsync(string message, string celPhone, string countryCode = "+55", string referenceId = null, DateTime? dateSend = null);
        /// <summary>
        /// ENVIO DE SMS PARA UM CELULAR COM POSSIBILIDADE DE AGENDAMENTO
        /// </summary>
        /// <param name="message">MENSAGEM SEM CARACTERES ESPECIAIS</param>
        /// <param name="celPhone">NUMERO DO CELULAR</param>
        /// <param name="countryCode">55</param>
        /// <param name="referenceId">REFERENCIA</param>
        /// <param name="dateSend">DATA DE ENVIO PARA AGENDAMENTO</param>
        /// <returns></returns>
        SmsDevViewModel SmsDevSendSms(string message, string celPhone, string countryCode = "+55", string referenceId = null, DateTime? dateSend = null);
        /// <summary>
        /// ENVIO DE SMS PARA VARIOS NUMEROS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        Task<List<SmsDevViewModel>> SmsDevSendMultipleSmsAsync(List<SmsDevMultipleViewModel> data, DateTime? dateSend = null);
        /// <summary>
        /// ENVIO DE SMS PARA VARIOS NUMEROS
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dateSend"></param>
        /// <returns></returns>
        List<SmsDevViewModel> SmsDevSendMultipleSms(List<SmsDevMultipleViewModel> data, DateTime? dateSend = null);
        /// <summary>
        /// METODO PARA OBTER STATUS DO ENVIO DE UM SMS 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SmsDevViewModel> GetAsync(string id);
        /// <summary>
        /// METODO PARA OBTER STATUS DO ENVIO DE UM SMS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SmsDevViewModel Get(string id);
    }
}