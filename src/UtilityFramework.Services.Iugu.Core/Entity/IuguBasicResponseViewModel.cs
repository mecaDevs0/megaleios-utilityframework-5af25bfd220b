using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class IuguBasicResponseViewModel : IuguBaseErrors
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}