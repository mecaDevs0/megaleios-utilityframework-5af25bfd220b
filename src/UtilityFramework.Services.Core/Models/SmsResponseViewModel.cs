using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Core.Models {
    public class SmsResponseViewModel {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty ("messages")]
        public List<SmsMessagesItemViewModel> Messages { get; set; }
        public bool Erro { get; set; }
        public string Message { get; set; }
        public string MessageEx { get; set; }

    }
}