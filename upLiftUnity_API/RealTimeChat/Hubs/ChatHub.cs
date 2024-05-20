using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using upLiftUnity_API.RealTimeChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upLiftUnity_API.RealTimeChat.Hubs
{
    public class ChatHub : Hub
    {


        static ConcurrentDictionary<string, string> dic = new ConcurrentDictionary<string, string>();

        public void Send(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public void SendToSpecific(string name, string message, string to)
        {
            if (dic.TryGetValue(to, out var connectionId))
            {
                Clients.Client(connectionId).SendAsync("broadcastMessage", name, message);
            }
        }

        public void Notify(string name, string id)
        {
            if (dic.ContainsKey(name))
            {
                Clients.Caller.SendAsync("differentName");
            }
            else
            {
                dic.TryAdd(name, id);
                foreach (var entry in dic)
                {
                    Clients.Caller.SendAsync("online", entry.Key);
                }
                Clients.Others.SendAsync("enters", name);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var name = dic.FirstOrDefault(x => x.Value == Context.ConnectionId.ToString());
            if (name.Key != null)
            {
                dic.TryRemove(name.Key, out _);
                await Clients.All.SendAsync("disconnected", name.Key);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
