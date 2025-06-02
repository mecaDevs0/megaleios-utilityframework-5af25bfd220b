
namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class AcquireResponseViewModel : BaseResponseViewModel
    {
        /// <summary>
        /// IDENTIFICADOR DO ARQUIVO (RESOURCE) 
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// IDENTIFICADOR DA GRAVAÇÃO
        /// </summary>
        /// <value></value>
        public string Sid { get; set; }
    }
}