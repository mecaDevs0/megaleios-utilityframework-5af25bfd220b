using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class TokenModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// Método
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}
