using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.WebSockets;

namespace UtilityFramework.Services.WebSocket.Core
{
    public static class WebSockedMiddleware
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                        PathString path,
                                                        WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
        }
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            services.AddSingleton(typeof(GenericSocketHandler));

            return services;
        }

        public static IApplicationBuilder UseCustomWebSocket(this IApplicationBuilder app, string pathSocket)
        {

            try
            {
                var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

                var webSocketOptions = new WebSocketOptions()
                {
                    KeepAliveInterval = TimeSpan.FromSeconds(120),
                    ReceiveBufferSize = 4 * 1024
                };

                app.UseWebSockets(webSocketOptions);
                app.MapWebSocketManager(pathSocket, serviceProvider.GetService<GenericSocketHandler>());
            }
            catch (Exception)
            {

            }
            return app;
        }
    }
}