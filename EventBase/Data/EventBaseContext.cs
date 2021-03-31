using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventBase.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventBase.Data
{
    public class EventBaseContext : IdentityDbContext<MyUser>
    {
        public EventBaseContext (DbContextOptions<EventBaseContext> options)
            : base(options)
        {
        }

        public DbSet<EventBase.Models.Event> Events { get; set; }
        public DbSet<MyUser> MyUsers { get; set; }
    }
}
