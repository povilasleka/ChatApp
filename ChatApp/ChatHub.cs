using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp
{
    public class ChatHub : Hub
    {
        private readonly IService<Connection> _connectionService;
        private readonly IService<Room> _roomService;

        public ChatHub(IService<Room> roomService, IService<Connection> connectionService)
        {
            _connectionService = connectionService;
            _roomService = roomService;
        }

        public async Task JoinRoom(Connection clientConnection)
        {
            if (!_roomService.Contains(r => r.Name == clientConnection.Room))
            {
                await Clients.Caller.SendAsync("JoinResponse", false, "Join failed!");
                return;
            }

            clientConnection.ConnectionId = Context.ConnectionId;
            _connectionService.Add(clientConnection);

            await Groups.AddToGroupAsync(Context.ConnectionId, clientConnection.Room);

            await Clients.Caller.SendAsync("JoinResponse", true, "Joined successfully");
            await Clients.Group(clientConnection.Room)
                .SendAsync("ReceiveMessage", "BOT", $"[+] {clientConnection.Client} joined the room!");
        }

        public async Task SendMessage(string message)
        {
            Connection connection = _connectionService.GetFirst(c => 
                c.ConnectionId == Context.ConnectionId);

            await Clients.Group(connection.Room)
                .SendAsync("ReceiveMessage", connection.Client, message);
        }
    }
}
