namespace UtilityFramework.Services.Core3.Models.AgoraIO
{
    public class SettingsViewModel
    {
        public string AppID { get; set; }
        public string AppCertificate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public StorageConfigViewModel StorageConfig { get; set; }
        public bool ShowContent { get; set; }
    }
}