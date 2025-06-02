using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class ClientRequestViewModel
    {
        /// <summary>
        /// IDENTIFICADOR DO ARQUIVO (RESOURCE)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ResourceId { get; set; }
        /// <summary>
        /// DURAÇÃO DO RESOURCE
        /// </summary>
        /// <value></value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ResourceExpiredHour { get; set; }
        /// <summary>
        /// PARAMETRO DE RETORNO RESPONSE STOP
        /// </summary>
        /// <value></value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? Async_Stop { get; set; }
        /// <summary>
        /// TOKEN DO CHANNEL
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        /// <summary>
        /// CONFIGURAÇOES DE GRAVAÇÃO
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RecordingConfigViewModel RecordingConfig { get; set; }
        /// <summary>
        /// CONFIGURAÇÕES DO ARQUIVO A SER SALVO
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RecordingFileConfigViewModel RecordingFileConfig { get; set; }
        /// <summary>
        /// CONFIGURAÇÕES DA NUVEM
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public StorageConfigViewModel StorageConfig { get; set; }
        /// <summary>
        /// CONFIGURAÇÕES DE CONVERSÃO E MIXAGEM DE ARQUIVOS
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TranscodeOptionsViewModel TranscodeOptions { get; set; }
    }
}