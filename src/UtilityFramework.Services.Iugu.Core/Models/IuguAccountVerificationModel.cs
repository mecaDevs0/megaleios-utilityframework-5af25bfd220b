using Newtonsoft.Json;
using UtilityFramework.Application.Core;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAccountVerificationModel : IuguBaseErrors
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [IsClass]
        public IuguAccountDataVerificationModel Data { get; set; }
        [JsonProperty("automatic_validation", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutomaticValidation { get; set; }
        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
        public IuguAccountFilesModel Files { get; set; }
    }
}