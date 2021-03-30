using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBase.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Event> HostedEvents { get; set; }
        public ICollection<Event> JoinedEvents { get; set; }
    }
}
