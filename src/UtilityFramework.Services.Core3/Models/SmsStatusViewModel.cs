using Newtonsoft.Json;

namespace UtilityFramework.Services.Core3.Models
{
    public class SmsStatusViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("groupid")]

        public int GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("groupname")]

        public string GroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]

        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]

        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("description")]

        public string Description { get; set; }
    }
}