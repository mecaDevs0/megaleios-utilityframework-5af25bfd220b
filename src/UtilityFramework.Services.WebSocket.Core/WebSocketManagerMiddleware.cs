using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UtilityFramework.Services.WebSocket.Core
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest == false)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            var connectionId = Guid.NewGuid().ToString();
            while (_webSocketHandler.HasClient(connectionId))
            {
                connectionId = Guid.NewGuid().ToString();
            }

            await _webSocketHandler.AddClient(new SocketClient()
            {
                Client = socket,
                Id = connectionId,
                Path = context.Request.Path.Value.TrimStart('/')
            });

            await _webSocketHandler.OnConnected(connectionId, socket);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    connectionId = await _webSocketHandler.OnDisconnected(socket);
                    await _webSocketHandler.RemoveClient(connectionId);
                    return;
                }

            });
        }

        private async Task Receive(System.Net.WebSockets.WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}