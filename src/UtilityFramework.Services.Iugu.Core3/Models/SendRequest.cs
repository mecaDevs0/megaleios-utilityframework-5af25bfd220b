using RestSharp;

namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class SendRequest
    {
        /// <summary>
        /// URL DO ENDPOINT
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// API TOKEN OU API RSA
        /// </summary>
        public string ApiToken { get; set; }
        /// <summary>
        /// METODO
        /// </summary>
        public Method Method { get; set; }
        /// <summary>
        /// BODY DA REQUEST
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// PRECISA DE ASINATURA RSA
        /// </summary>
        public bool NeedSignature { get; set; }
    }
}