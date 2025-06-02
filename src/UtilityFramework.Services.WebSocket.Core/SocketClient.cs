using System.Net.WebSockets;

namespace UtilityFramework.Services.WebSocket.Core
{
    public class SocketClient
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public TypeAction Event { get; set; }
        public System.Net.WebSockets.WebSocket Client { get; set; }
    }
}