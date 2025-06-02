using System.Net;

namespace UtilityFramework.Application.Core3.ViewModels
{
    public class ReturnApiViewModel
    {
        /// <summary>
        /// BODY
        /// </summary>
        /// <value></value>
        public string Content { get; set; }
        /// <summary>
        /// HTTP STATUS
        /// </summary>
        /// <value></value>
        public HttpStatusCode StatusCode { get; set; }
    }
}