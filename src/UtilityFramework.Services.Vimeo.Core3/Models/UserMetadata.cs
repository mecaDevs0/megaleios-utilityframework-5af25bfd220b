﻿
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Vimeo.Core3.Models
{
    /// <summary>
    /// User  metadata
    /// </summary>
    public class UserMetadata
    {
        /// <summary>
        /// Connections
        /// </summary>
        [PublicAPI]
        [JsonProperty(PropertyName = "connections")]
        public UserConnections Connections { get; set; }

        /// <summary>
        /// Interactions
        /// </summary>
        // [PublicAPI]
        // [JsonProperty(PropertyName = "interactions")]
        // public UserInteractions Interactions { get; set; }

        /// <summary>
        /// Follower
        /// </summary>
        //[PublicAPI]
        //[JsonProperty(PropertyName = "follower")]
        //public Follower Follower { get; set; }
    }
}