using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UtilityFramework.Services.Core3.Models
{
    public class SmsDevViewModel
    {
        [JsonProperty("situacao")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeSituation Situacao { get; set; }

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("refer")]
        public string Refer { get; set; }
        [JsonProperty("data_envio")]
        public string DataEnvio { get; set; }
        [JsonProperty("operadora")]
        public string Operadora { get; set; }
    }
}