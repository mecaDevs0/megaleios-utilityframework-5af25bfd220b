using System.Collections.Generic;

namespace UtilityFramework.Services.Core3.Models
{
    public class Config
    {
        public Config()
        {
            ONESIGNAL = new List<ONESIGNAL>();
            SMTP = new SENDER();
        }
        public SENDER SMTP { get; set; }
        public List<ONESIGNAL> ONESIGNAL { get; set; }

    }

    public class SENDER
    {
        public string TEMPLATE { get; set; }
        public string HOST { get; set; }
        public int PORT { get; set; }
        public bool SSL { get; set; }
        public string EMAIL { get; set; }
        public string NAME { get; set; }
        public string PASSWORD { get; set; }
        public string LOGIN { get; set; }
        public bool USEAUTH { get; set; } = true;
    }

    public class ONESIGNAL
    {
        public string KEY { get; set; }
        public string SOUND { get; set; } = "default";
        public string ICON { get; set; } = "default";
        public string APPID { get; set; }

    }
}