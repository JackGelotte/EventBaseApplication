using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EventBase.Data
{
    public static class DbSeed
    {
        public static void Seeder(EventBaseContext context)
        {
            
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Events.RemoveRange(context.Events);
            context.SaveChanges();
            var events = new Event[]
            {
                new Event{Title="DreamHack", Description="Mega Lan Party", Place="Jönköping", Address="Elmia", Date=DateTime.Parse("2022-06-15"), SpotsAvailable=42},
            };

            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}
