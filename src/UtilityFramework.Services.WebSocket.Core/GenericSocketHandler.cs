using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UtilityFramework.Application.Core;

namespace UtilityFramework.Services.WebSocket.Core
{
    public class GenericSocketHandler : WebSocketHandler
    {
        public GenericSocketHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager) { }

        public override Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new System.NotImplementedException();
        }


        public async Task SocketNotifyAsync(string path, object data, TypeAction typeAction = TypeAction.child_changed, bool inTask = true)
        {
            try
            {
                if (inTask)
                {
                    var _ = Task.Run(async () => await NotifyAsync(path, data, typeAction));
                }
                else
                {
                    await NotifyAsync(path, data, typeAction);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }

        private async Task NotifyAsync(string path, object data, TypeAction typeAction = TypeAction.child_changed)
        {
            try
            {
                var parts = path.TrimStart('/').Split('/').ToArray();
                var replaceText = "";
                var last = parts.LastOrDefault();

                if (string.IsNullOrEmpty(last) == false && last.Length != 24)
                    replaceText = $"/{last}";

                var messageDataFull = new DefaultResponseViewModel()
                {
                    Data = data,
                    Action = typeAction,
                };
                var messageDataProperty = new DefaultResponseViewModel()
                {
                    Data = data,
                    Action = typeAction,
                };
                var isUpdateChild = false;
                if (string.IsNullOrEmpty(path) == false)
                {
                    var notifyClient = await GetClientByPath(path.Replace(replaceText, "").ToLower());

                    if (notifyClient.Count > 0)
                    {


                        if (parts.Count() > 0 && (parts.Count() % 2) != 0)
                        {
                            /*OBSERVER ONE PROPERTY*/
                            var childName = last;

                            if (string.IsNullOrEmpty(childName) == false)
                            {
                                isUpdateChild = true;
                                messageDataProperty.Data = Utilities.GetValueByProperty(data, childName);
                            }
                            messageDataProperty.Key = messageDataFull.Key = parts[parts.Length - 2];

                        }


                        for (int i = 0; i < notifyClient.Count; i++)
                        {
                            await SendMessageAsync(notifyClient[i].Client, JsonConvert.SerializeObject(notifyClient[i].Path == path && isUpdateChild ? messageDataProperty : messageDataFull));
                        }
                    }
                }
                else
                {
                    await SendMessageToAllAsync(JsonConvert.SerializeObject(messageDataFull));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }
    }
}