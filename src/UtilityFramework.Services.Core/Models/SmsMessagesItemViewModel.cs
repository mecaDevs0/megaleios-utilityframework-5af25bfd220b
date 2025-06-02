using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models
{
    public class SmsMessagesItemViewModel
    {
        /// <summary>
        /// 
        /// </summary>
       [JsonProperty("to")]
       public string To { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status")]
        public SmsStatusViewModel SmsStatusViewModel { get; set; }
        [JsonProperty("smsCount")]

        public int SmsCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("messageId")]

        public string MessageId { get; set; }
    }
}