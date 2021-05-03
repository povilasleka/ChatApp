using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(ClientConnection clientConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, clientConnection.Room);

            await Clients.Group(clientConnection.Room)
                .SendAsync("ReceiveMessage", $"[+] {clientConnection.Client} joined the room!");
        }
    }
}
