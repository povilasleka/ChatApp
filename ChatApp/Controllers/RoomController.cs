using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers 
{
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly ConcurrentBag<Room> _rooms;

        public RoomController(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        public ActionResult<IEnumerable<Room>> Index()
        {
           return _rooms;
        }

        public ActionResult<Room> Get(string roomName) 
        {
            return _rooms.FirstOrDefault(r => r.Name == roomName);
        }
    }
}