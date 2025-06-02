using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace UtilityFramework.Services.WebSocket.Core
{
    public class DefaultResponseViewModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeAction Action { get; set; }
        public object Data { get; set; }
        public string Key { get; set; }
    }
}