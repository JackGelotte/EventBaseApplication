using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBase.Models;

namespace EventBase.Data
{
    public static class DbSeed
    {
        public static void Seeder(EventBaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var events = new Event[]
            {
                new Event{Title="DreamHack", Description="Mega Lan Party", Place="Jönköping", Address="Elmia", Date=DateTime.Parse("2022-06-15"), SpotsAvailable=42},
            };

            context.Event.AddRange(events);
            context.SaveChanges();
        }
    }
}
