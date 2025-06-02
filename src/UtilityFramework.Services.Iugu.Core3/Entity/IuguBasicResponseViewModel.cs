using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class IuguBasicResponseViewModel : IuguBaseErrors
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}