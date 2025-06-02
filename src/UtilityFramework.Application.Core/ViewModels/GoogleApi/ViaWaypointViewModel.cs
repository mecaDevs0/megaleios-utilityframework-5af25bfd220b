using Newtonsoft.Json;

namespace UtilityFramework.Application.Core.ViewModels.GoogleApi
{
    public class ViaWaypointViewModel
    {
        [JsonProperty("location")]
        public LocationViewModel Location { get; set; }

        [JsonProperty("step_index")]
        public int StepIndex { get; set; }

        [JsonProperty("step_interpolation")]
        public double StepInterpolation { get; set; }
    }



}