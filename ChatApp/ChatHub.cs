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
        private readonly HashSet<Room> _rooms;
        private readonly HashSet<ClientConnection> _clientConnections;

        public ChatHub(SingletonManager sm)
        {
            _rooms = sm.Rooms;
            _clientConnections = sm.ClientConnections;
        }

        public async Task JoinRoom(ClientConnection clientConnection)
        {
            if (!_rooms.Any(r => r.Name == clientConnection.Room))
            {
                await Clients.Caller.SendAsync("JoinResponse", false, "Room does not exist");
                return;
            }
            _clientConnections.Add(clientConnection);

            await Groups.AddToGroupAsync(Context.ConnectionId, clientConnection.Room);

            await Clients.Caller.SendAsync("JoinResponse", true, "Joined successfully");
            await Clients.Group(clientConnection.Room)
                .SendAsync("ReceiveMessage", "BOT", $"[+] {clientConnection.Client} joined the room!");
        }

        public async Task SendMessage(ClientConnection clientConnection, string message)
        {
            await Clients.Group(clientConnection.Room)
                .SendAsync("ReceiveMessage", clientConnection.Client, message);
        }
    }
}
