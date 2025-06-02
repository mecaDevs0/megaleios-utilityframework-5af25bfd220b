namespace UtilityFramework.Services.Core.Models.AgoraIO.Common
{
    public interface IPackable
    {
        ByteBuf marshal(ByteBuf outBuf);
    }
}
