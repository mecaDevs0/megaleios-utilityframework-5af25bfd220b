

using System.Net;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class BaseResponseViewModel
    {
        public bool HasError { get; set; }
        /// <summary>
        /// CÓDIGO DO ERRO
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// Mensagem
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Mensagem de erro (Message|Reason)
        /// </summary>
        public string MessageError { get; set; }

        /// <summary>
        /// MOTIVO
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// RESPONSE EM STRING
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Server Response
        /// </summary>

        public ServerResponseViewModel ServerResponse { get; set; }

        /// <summary>
        /// METODO PARA VERIFICAR SE EXISTE CÓDIGO DE ERRO E MAPPEAR O CAMPO MESSAGEERROR COM O ERRO
        /// </summary>
        /// <param name="statusCode"></param>
        public void CheckHasError(HttpStatusCode? statusCode = null)
        {
            HasError = Code != null || (statusCode != null && statusCode != HttpStatusCode.OK);
            MessageError = string.IsNullOrEmpty(Message) == false ? Message : Reason;

            if (HasError && string.IsNullOrEmpty(MessageError))
                MessageError = $"{ServerResponse?.Command} {ServerResponse?.Payload?.Message}".Trim();
        }
    }
}