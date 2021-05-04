using System.Collections.Concurrent;
using ChatApp.Models;

namespace ChatApp 
{
    public class SingletonManager
    {
        public SingletonManager()
        {
            Rooms = new ConcurrentBag<Room>();
        }

        public ConcurrentBag<Room> Rooms { get; }
    }
}