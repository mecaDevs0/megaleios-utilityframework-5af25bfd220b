using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.ViewModels.GoogleApi
{
    public class BoundsViewModel
    {
        [JsonProperty("northeast")]
        public NortheastViewModel Northeast { get; set; }

        [JsonProperty("southwest")]
        public SouthwestViewModel Southwest { get; set; }
    }



}