using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UtilityFramework.Services.Iugu.Core3.Enums;

namespace UtilityFramework.Services.Iugu.Core3.Request
{

    public class InviteViewModel
    {

        [JsonProperty("permissions")]
        [JsonConverter(typeof(StringEnumConverter))]
        public List<Permission> Permissions { get; set; } = new List<Permission>();

        [JsonProperty("email")]
        public string Email { get; set; }
    }

}