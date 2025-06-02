using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UtilityFramework.Services.WebSocket.Core
{
    public abstract class WebSocketHandler
    {
        protected ConnectionManager WebSocketConnectionManager { get; set; }
        private static List<SocketClient> _socketClients = new List<SocketClient>();

        public WebSocketHandler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        public Task AddClient(SocketClient client)
        {
            _socketClients.Add(client);
            return Task.CompletedTask;
        }

        public bool HasClient(string connectionId) => _socketClients.Count(x => x.Id == connectionId) > 0;
        public Task RemoveClient(string connectionId)
        {
            _socketClients = _socketClients.Where(x => x.Id != connectionId).ToList();
            return Task.CompletedTask;
        }

        public Task<SocketClient> GetClientById(string connectionId) => Task.FromResult(_socketClients.Find(x => x.Id == connectionId));
        public Task<List<SocketClient>> GetClientByPath(string path, TypeAction typeAction = TypeAction.child_changed) => Task.FromResult(_socketClients.Where(x => x.Path.IndexOf(path, StringComparison.OrdinalIgnoreCase) >= 0).ToList());


        public virtual Task OnConnected(string connectionId, System.Net.WebSockets.WebSocket socket)
        {
            WebSocketConnectionManager.AddSocket(connectionId, socket);
            return Task.CompletedTask;
        }

        public virtual async Task<string> OnDisconnected(System.Net.WebSockets.WebSocket socket)
        {
            var connectionId = WebSocketConnectionManager.GetId(socket);
            await WebSocketConnectionManager.RemoveSocket(connectionId);

            return connectionId;
        }

        public async Task SendMessageAsync(System.Net.WebSockets.WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true,
                                    cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message) => await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        public abstract Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}