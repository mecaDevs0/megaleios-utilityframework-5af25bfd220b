﻿using System;
using UtilityFramework.Services.Core3.Models.AgoraIO.Common;

namespace UtilityFramework.Services.Core3.Models.AgoraIO.Utils
{
    public class Utils
    {
        public static int getTimestamp()
        {
            TimeSpan t = DateTime.Now - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }

        public static int randomInt()
        {
            return new Random().Next();
        }

        public static byte[] pack(PrivilegeMessage packableEx)
        {
            ByteBuf buffer = new ByteBuf();
            packableEx.marshal(buffer);
            return buffer.asBytes();
        }
        public static byte[] pack(IPackable packableEx)
        {
            ByteBuf buffer = new ByteBuf();
            packableEx.marshal(buffer);
            return buffer.asBytes();
        }

        public static string base64Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
