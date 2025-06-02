using Firebase.Auth;
using Newtonsoft.Json;
using UtilityFramework.Services.Core.Enum;

namespace UtilityFramework.Services.Core.Models
{
    public class FirebaseConfigViewModel
    {
        [JsonProperty(PropertyName = "urlDataBase")]
        public string UrlDataBase { get; set; }
        [JsonProperty(PropertyName = "apiKey")]

        public string ApiKey { get; set; }
        [JsonProperty(PropertyName = "socialKey")]

        public string SocialKey { get; set; }
        [JsonProperty(PropertyName = "email")]

        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]

        public string Password { get; set; }
        [JsonProperty(PropertyName = "useAuth")]

        public bool UseAuth { get; set; }
        [JsonProperty(PropertyName = "typeAuth")]

        public TypeAuthFirebase TypeAuth { get; set; }
        [JsonProperty(PropertyName = "firebaseAuthType")]

        public FirebaseAuthType FirebaseAuthType { get; set; }
    }
}