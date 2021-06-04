using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class RoomService : IService<Room>
    {
        private readonly HashSet<Room> _rooms;
        private readonly IHubContext<ChatHub> _hubContext;

        public RoomService(IHubContext<ChatHub> hubContext)
        {
            _rooms = new HashSet<Room>();
            _hubContext = hubContext;
        }

        public IEnumerable<Room> GetAll()
        {
            return _rooms;
        }

        public bool Add(Room room)
        {
            return _rooms.Add(room);
        }

        public Room GetFirst(Func<Room, bool> predicate)
        {
            return _rooms.FirstOrDefault(predicate);
        }

        public IEnumerable<Room> GetWhere(Func<Room, bool> predicate)
        {
            return _rooms.Where(predicate);
        }

        public bool RemoveFirst(Func<Room, bool> predicate)
        {
            Room roomToRemove = _rooms.FirstOrDefault(predicate);
            return _rooms.Remove(roomToRemove);
        }

        public int RemoveWhere(Func<Room, bool> predicate)
        {
            return _rooms.RemoveWhere(new Predicate<Room>(predicate));
        }

        public bool Contains(Func<Room, bool> predicate)
        {
            return _rooms.Any(predicate);
        }
    }
}
