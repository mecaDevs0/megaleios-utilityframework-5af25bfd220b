namespace UtilityFramework.Services.Core3.Models.AgoraIO.Common
{
    public interface IPackable
    {
        ByteBuf marshal(ByteBuf outBuf);
    }
}
