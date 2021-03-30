using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventBase.Data
{
    public class EventBaseContext : IdentityDbContext<User>
    {
        public EventBaseContext (DbContextOptions<EventBaseContext> options)
            : base(options)
        {
        }

        public DbSet<EventBase.Models.Event> Event { get; set; }
    }
}
