using System.Collections.Concurrent;
using System.Collections.Generic;
using ChatApp.Models;

namespace ChatApp 
{
    public class SingletonManager
    {
        public SingletonManager()
        {
            Rooms = new HashSet<Room>();
            ClientConnections = new HashSet<Connection>();
        }

        public HashSet<Room> Rooms { get; }
        public HashSet<Connection> ClientConnections { get; }
    }
}