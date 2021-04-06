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
        public static void Seeder(EventBaseContext context, UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            {
                SeedEvent(context);
                SeedRoles(roleManager);
                SeedUsers(userManager);

            }
        }
        static void SeedUsers(UserManager<MyUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.FirstName = "Admin";
                user.LastName = "Adminsson";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("organizer@organizer.com").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "organizer@organizer.com";
                user.Email = "organizer@organizer.com";
                user.FirstName = "Organizer";
                user.LastName = "Organizersson";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Organizer123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Organizer").Wait();
                }
            }
            if (userManager.FindByEmailAsync("user@user.com").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "member@member.com";
                user.Email = "member@member.com";
                user.FirstName = "Member";
                user.LastName = "Membersson";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Member123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }


            
        }

        static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Organizer", "Member" };


            foreach (var roleName in roleNames)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }

        }
        static void SeedEvent(EventBaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Events.RemoveRange(context.Events);
            context.SaveChanges();
            var events = new Event[]
            {
                new Event{Title="DreamHack", Description="Mega Lan Party", Place="Jönköping", Address="Elmia", Date=DateTime.Parse("2022-06-15"), SpotsAvailable=42},
                new Event{Title="CodeWars", Description="Fika med Grabs", Place="Göteborg", Address="Majorna osv.", Date=DateTime.Parse("2022-08-23"), SpotsAvailable=0},
                new Event{Title="Föreläsning", Description="Bootstrap aka. helvetet", Place="Helvetet", Address="Innersta Ringen", Date=DateTime.Parse("2023-09-01"), SpotsAvailable=1},
                new Event{Title="Afterwork hos Richalito", Description="Öl med lite Warhammer", Place="Varberg", Address="Varberg Fästning", Date=DateTime.Parse("2029-02-28"), SpotsAvailable=25},
            };

            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}

