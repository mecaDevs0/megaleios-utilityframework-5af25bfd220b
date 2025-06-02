using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Core.Models.AgoraIO
{
    public class AuthenticateRequest
    {

        /// <summary>
        /// Canal (required)
        /// </summary>
        [Display(Name = "Canal")]
        public string Channel { get; set; }
        /// <summary>
        /// Identificador do usuário (required)
        /// </summary>
        [Display(Name = "Identificador do usuário")]
        public string Uid { get; set; }

        public uint ExpiredTs { get; set; } = 0;

        public int Role { get; set; } = 1;
    }
}