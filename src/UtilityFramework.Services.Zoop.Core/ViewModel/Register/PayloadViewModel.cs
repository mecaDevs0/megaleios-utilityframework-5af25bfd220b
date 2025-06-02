using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class PayloadViewModel<T>
    {

        [JsonProperty("object")]
        public T Object { get; set; }
    }
}