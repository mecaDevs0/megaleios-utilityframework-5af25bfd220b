using Newtonsoft.Json;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class ZoopPaginationViewModel
    {
        [JsonProperty("limit")]
        public int? Limit { get; set; }
        [JsonProperty("offSet")]
        public int? OffSet { get; set; }
        [JsonProperty("sort")]
        public string Sort { get; set; }
        [JsonProperty("date_range")]
        public long? DateRange { get; set; }
        [JsonProperty("date_range[gt]")]
        public long? DateRangeGT { get; set; }
        [JsonProperty("date_range[gte]")]
        public long? DateRangeGTE { get; set; }
        [JsonProperty("date_range[lt]")]
        public long? DateRangeLT { get; set; }
        [JsonProperty("date_range[lte]")]
        public long? DateRangeLTE { get; set; }
    }
}