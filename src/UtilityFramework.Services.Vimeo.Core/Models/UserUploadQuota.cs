using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Vimeo.Core.Models
{
    /// <summary>
    /// Space
    /// </summary>
    public class Space
    {
        /// <summary>
        /// Max
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "max")]
        public long Max { get; set; }

        /// <summary>
        /// Free
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "free")]
        public long Free { get; set; }

        /// <summary>
        /// Used
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "used")]
        public long Used { get; set; }
    }

    /// <summary>
    /// User upload quota
    /// </summary>
    public class UserUploadQuota
    {
        /// <summary>
        /// Space
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "space")]
        public Space Space { get; set; }

        /// <summary>
        /// Resets
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "resets")]
        public int Resets { get; set; }

        /// <summary>
        /// Quota
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "quota")]
        public UserQuota Quota { get; set; }
    }
}