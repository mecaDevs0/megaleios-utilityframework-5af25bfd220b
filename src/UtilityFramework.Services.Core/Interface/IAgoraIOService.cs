using System.Collections.Generic;
using System.Threading.Tasks;

using UtilityFramework.Services.Core.Models.AgoraIO;
using UtilityFramework.Services.Core.Models.AgoraIO.Media;

namespace UtilityFramework.Services.Core.Interface
{
    public interface IAgoraIOService
    {
        /// <summary>
        /// METODO PARA GERAR RESOURCEID DE UMA LIVE
        /// </summary>
        /// <returns></returns>
        Task<AcquireResponseViewModel> AcquireResourceId(AcquireRequestViewModel model);
        /// <summary>
        /// METODO PARA SALVAR GRAVAÇÃO
        /// </summary>
        /// <returns></returns>
        Task<StopResponseViewModel> StopRecording(string resourceId, string sid, StopRequestViewModel model);
        /// <summary>
        /// METODO PARA OBTER STATUS DA GRAVAÇÃO
        /// </summary>
        /// <returns></returns>
        Task<StopResponseViewModel> Query(string resourceId, string sid);
        /// <summary>
        /// METODO PARA INICIAR A GRAVAÇÃO DA LIVE
        /// </summary>
        /// <returns></returns>
        Task<AcquireResponseViewModel> StartRecording(string resourceId, AcquireRequestViewModel model);
        /// <summary>
        /// GERAR TOKEN DE ACESSO
        /// </summary>
        /// <param name="channnelId"></param>
        /// <param name="uid"></param>
        /// <param name="ts"></param>
        /// <param name="salt"></param>
        /// <param name="expiredTs"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        string GenerateToken(string channnelId, string uid, uint ts, uint salt, uint expiredTs, List<Privileges> privileges);
        /// <summary>
        /// GERAR TOKEN DE ACESSO
        /// </summary>
        /// <param name="channnelId"></param>
        /// <param name="uid"></param>
        /// <param name="expiredTs"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        string GenerateToken(string channnelId, string uid, uint expiredTs, List<Privileges> privileges);
    }
}