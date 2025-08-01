﻿namespace UtilityFramework.Services.Core.Models.AgoraIO.Common
{
    public class PackContent : IPackable
    {
        public byte[] signature;
        public uint crcChannelName;
        public uint crcUid;
        public byte[] rawMessage;

        public PackContent()
        {
        }

        public PackContent(byte[] signature, uint crcChannelName, uint crcUid, byte[] rawMessage)
        {
            this.signature = signature;
            this.crcChannelName = crcChannelName;
            this.crcUid = crcUid;
            this.rawMessage = rawMessage;
        }


        public ByteBuf marshal(ByteBuf outBuf)
        {
            return outBuf.put(signature).put(crcChannelName).put(crcUid).put(rawMessage);
        }


        public void unmarshal(ByteBuf inBuf)
        {
            signature = inBuf.readBytes();
            crcChannelName = inBuf.readInt();
            crcUid = inBuf.readInt();
            rawMessage = inBuf.readBytes();
        }
    }
}
