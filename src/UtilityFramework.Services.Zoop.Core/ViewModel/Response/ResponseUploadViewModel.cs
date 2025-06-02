using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.ViewModel.Register;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class ResponseUploadViewModel : BaseErrorViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("mime_type")]
        public string MimeType { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("category")]
        public object Category { get; set; }

        [JsonProperty("uploaded_by")]
        public string UploadedBy { get; set; }

        [JsonProperty("owner")]
        public SellerViewModel Owner { get; set; }

        [JsonProperty("uploaded_ip")]
        public object UploadedIp { get; set; }

        [JsonProperty("metadata")]
        public MetaDataViewModel Metadata { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}