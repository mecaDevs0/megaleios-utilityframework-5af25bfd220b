using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class ServerResponseViewModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FileListMode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<FileListViewModel> FileList { get; set; } = new List<FileListViewModel>();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UploadingStatus { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Command { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PayloadViewModel Payload { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int SubscribeModeBitmask { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Vid { get; set; }
    }
}
