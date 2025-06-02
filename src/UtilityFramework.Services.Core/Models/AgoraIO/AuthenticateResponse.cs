using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class AuthenticateResponse
    {
        /// <summary>
        /// Canal
        /// </summary>
        [Display(Name = "Canal")]
        public string Channel { get; set; }
        /// <summary>
        /// Identificador o usuário
        /// </summary>
        [Display(Name = "Identificador o usuário")]
        public string Uid { get; set; }
        /// <summary>
        /// Token de acesso
        /// </summary>
        [Display(Name = "Token de acesso")]
        public string Token { get; set; }
    }
}