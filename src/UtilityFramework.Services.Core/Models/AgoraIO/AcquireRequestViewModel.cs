
namespace UtilityFramework.Services.Core.Models.AgoraIO
{

    public class AcquireRequestViewModel
    {
        /// <summary>
        /// IDENTIFICADOR DO CHANNEL
        /// </summary>
        public string Cname { get; set; }
        /// <summary>
        /// IDENTIFICADOR DO USUÁRIO
        /// </summary>
        /// <value></value>
        public string Uid { get; set; }
        /// <summary>
        /// IDENTIFICADOR DO ARQUIVO (RESOURCE) 
        /// </summary>  
        public ClientRequestViewModel ClientRequest { get; set; }
    }
}