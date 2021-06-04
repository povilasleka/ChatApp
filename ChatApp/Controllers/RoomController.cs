using System.Collections.Generic;
using System.Linq;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly IService<Room> _roomService;
        public RoomController(IService<Room> roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> Index()
        {
            return _roomService.GetAll().ToList();
        }

        [HttpGet("{roomName}")]
        public ActionResult<Room> Get(string roomName) 
        {
            return _roomService.GetFirst(r => r.Name == roomName);
        }

        [HttpPost]
        public IActionResult Create(Room room) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Room model is not valid.");
            }

            if (_roomService.GetAll().Any(r => r.Name == room.Name))
            {
                return BadRequest("Room already exists.");
            }

            _roomService.Add(room);

            return Created($"room/{room.Name}", room);
        }

        [HttpDelete("{roomName}")]
        public IActionResult Remove(string roomName) 
        {
            if (!_roomService.Contains(r => r.Name == roomName))
            {
                return BadRequest("Room does not exist.");
            }

            _roomService.RemoveFirst(r => r.Name == roomName);

            return NoContent();
        }
    }
}