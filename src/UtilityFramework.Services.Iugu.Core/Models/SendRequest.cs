using RestSharp;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class SendRequest
    {
        public string Url { get; set; }
        public string ApiToken { get; set; }
        public Method Method { get; set; }
        public object Data { get; set; }
        public bool NeedSignature { get; set; }
    }
}