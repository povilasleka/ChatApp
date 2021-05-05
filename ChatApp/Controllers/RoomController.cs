using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly HashSet<Room> _rooms;
        public RoomController(IHubContext<ChatHub> chatHubContext, SingletonManager sm)
        {
            _chatHubContext = chatHubContext;
            _rooms = sm.Rooms;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> Index()
        {
           return _rooms;
        }

        [HttpGet("{roomName}")]
        public ActionResult<Room> Get(string roomName) 
        {
            return _rooms.FirstOrDefault(r => r.Name == roomName);
        }

        [HttpPost]
        public IActionResult Create(Room room) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Room model is not valid.");
            }

            if (_rooms.Any(r => r.Name == room.Name))
            {
                return BadRequest("Room already exists.");
            }

            _rooms.Add(room);

            return Created($"room/{room.Name}", room);
        }

        [HttpDelete("{roomName}")]
        public IActionResult Remove(string roomName) 
        {
            if (!_rooms.Any(r => r.Name == roomName))
            {
                return BadRequest("Room does not exist.");
            }

            Room roomToRemove = _rooms.First(r => r.Name == roomName);
            _rooms.Remove(roomToRemove);

            return NoContent();
        }
    }
}