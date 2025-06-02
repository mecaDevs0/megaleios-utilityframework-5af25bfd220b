using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class RecordingFileConfigViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AvFileType { get; set; } = new List<string>();
    }
}