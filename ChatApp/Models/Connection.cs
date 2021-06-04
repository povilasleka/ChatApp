using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public struct Connection
    {
        public string ConnectionId { get; set; }
        public string Client { get; set; }
        public string Room { get; set; }
    }
}
