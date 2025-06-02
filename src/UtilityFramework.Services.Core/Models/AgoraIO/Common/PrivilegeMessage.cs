using System.Collections.Generic;

namespace UtilityFramework.Services.Core.Models.AgoraIO.Common
{
    public class PrivilegeMessage : IPackable
    {
        public uint salt;
        public uint ts;
        public Dictionary<ushort, uint> messages;
        public PrivilegeMessage()
        {
            salt = (uint)Utils.Utils.randomInt();
            ts = (uint)(Utils.Utils.getTimestamp() + 24 * 3600);
            messages = new Dictionary<ushort, uint>();
        }

        public ByteBuf marshal(ByteBuf outBuf)
        {
            return outBuf.put(salt).put(ts).putIntMap(messages);
        }

        public void unmarshal(ByteBuf inBuf)
        {
            salt = inBuf.readInt();
            ts = inBuf.readInt();
            messages = inBuf.readIntMap();
        }
    }
}
