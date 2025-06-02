using System;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguCredentials
    {
        public string AccoundId { get; set; }
        public string LiveKeyRSA { get; set; }
        public string LiveKey { get; set; }
        public string TestKey { get; set; }
        public string KeyUsage { get; set; }
        public bool Sandbox { get; set; }
        public bool ShowContent { get; set; }
        public string PrivateKeyPath { get; set; } = "/content/files/private_key.pem";
        public void SetKeyUsage()
        {
            if (string.IsNullOrEmpty(AccoundId) == false)
            {
                if (string.IsNullOrEmpty(TestKey) && string.IsNullOrEmpty(LiveKey))
                    throw new ArgumentNullException("Configure o campo IUGU no appsettings.*.json  ou no Settings/Config.json");
            }
            KeyUsage = Sandbox ? TestKey : LiveKey;
        }
    }
}