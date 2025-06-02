using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class CustomVariablesModel
    {
        /// <summary>
        /// Nome do atributo
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Valor do atributo
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
        /// <summary>
        /// Usado para remover
        /// </summary>
        [JsonProperty("_destroy")]
        public bool Destroy { get; set; }
    }
}
