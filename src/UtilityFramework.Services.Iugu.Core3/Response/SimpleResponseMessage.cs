using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class SimpleResponseMessage : IuguBaseErrors
    {
        /// <summary>
        /// Result of request
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Mensagem
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
