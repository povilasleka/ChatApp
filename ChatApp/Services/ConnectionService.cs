using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Services
{
    public class ConnectionService : IService<Connection>
    {
        private HashSet<Connection> _connections;

        public ConnectionService()
        {
            _connections = new HashSet<Connection>();
        }

        public bool Add(Connection entity)
        {
            return _connections.Add(entity);
        }

        public bool Contains(Func<Connection, bool> predicate)
        {
            return _connections.Any(predicate);
        }

        public IEnumerable<Connection> GetAll()
        {
            return _connections.AsEnumerable();
        }

        public Connection GetFirst(Func<Connection, bool> predicate)
        {
            return _connections.FirstOrDefault(predicate);
        }

        public IEnumerable<Connection> GetWhere(Func<Connection, bool> predicate)
        {
            return _connections.Where(predicate).AsEnumerable();
        }

        public bool RemoveFirst(Func<Connection, bool> predicate)
        {
            Connection connToRemove = _connections.FirstOrDefault(predicate);
            return _connections.Remove(connToRemove);
        }

        public int RemoveWhere(Func<Connection, bool> predicate)
        {
            return _connections.RemoveWhere(new Predicate<Connection>(predicate));
        }
    }
}
