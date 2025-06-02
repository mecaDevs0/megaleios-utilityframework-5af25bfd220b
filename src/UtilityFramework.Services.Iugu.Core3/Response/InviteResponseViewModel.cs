using System.Collections.Generic;

using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Response
{
    public class InviteResponseViewModel : IuguBaseErrors
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("invited_by")]
        public string InvitedBy { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("used")]
        public object Used { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; } = new List<string>();

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("invited_by_email")]
        public string InvitedByEmail { get; set; }
    }

}