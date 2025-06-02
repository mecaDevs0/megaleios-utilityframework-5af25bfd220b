using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Response
{
    public class SimpleResponseMessage : IuguBaseErrors
    {
        /// <summary>
        /// Result of request
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
