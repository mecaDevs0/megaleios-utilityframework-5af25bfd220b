using System.Collections.Generic;

namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class StorageConfigViewModel
    {
        public int Vendor { get; set; }
        public int Region { get; set; }
        public string Bucket { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public List<string> FileNamePrefix { get; set; } = new List<string>();
    }
}