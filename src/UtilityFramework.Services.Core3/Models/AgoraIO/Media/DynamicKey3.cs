using System;
using System.IO;
using UtilityFramework.Services.Core3.Models.AgoraIO.Extensions;
using UtilityFramework.Services.Core3.Models.AgoraIO.Utils;

namespace UtilityFramework.Services.Core3.Models.AgoraIO.Media
{
    public class DynamicKey3
    {
        public static string generate(string appID, string appCertificate, string channelName, int unixTs, int randomInt, long uid, int expiredTs) //throws Exception
        {
            string version = "003";
            string unixTsStr = ("0000000000" + unixTs).Substring(unixTs.ToString().Length);
            string randomIntStr = ("00000000" + randomInt.ToString("x4")).Substring(randomInt.ToString("x4").Length);
            uid = uid & 0xFFFFFFFFL;
            string uidStr = ("0000000000" + uid.ToString()).Substring(uid.ToString().Length);
            string expiredTsStr = ("0000000000" + expiredTs.ToString()).Substring(expiredTs.ToString().Length);
            string signature = generateSignature3(appID, appCertificate, channelName, unixTsStr, randomIntStr, uidStr, expiredTsStr);
            return string.Format("{0}{1}{2}{3}{4}{5}{6}", version, signature, appID, unixTsStr, randomIntStr, uidStr, expiredTsStr);
        }

        public static string generateSignature3(string appID, string appCertificate, string channelName, string unixTsStr, string randomIntStr, string uidStr, string expiredTsStr)// throws Exception
        {
            using (var ms = new MemoryStream())
            using (BinaryWriter baos = new BinaryWriter(ms))
            {
                baos.Write(appID.GetByteArray());
                baos.Write(unixTsStr.GetByteArray());
                baos.Write(randomIntStr.GetByteArray());
                baos.Write(channelName.GetByteArray());
                baos.Write(uidStr.GetByteArray());
                baos.Write(expiredTsStr.GetByteArray());
                baos.Flush();

                byte[] sign = DynamicKeyUtil.encodeHMAC(appCertificate, ms.ToArray());
                return DynamicKeyUtil.bytesToHex(sign);
            }
        }
    }
}
