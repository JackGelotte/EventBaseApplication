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
            if (userManager.FindByEmailAsync("user@user.com").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "user@user.com";
                user.Email = "user@user.com";
                user.FirstName = "User";
                user.LastName = "User";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "User123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }


            if (userManager.FindByEmailAsync("admin@admin.se").Result == null)
            {
                MyUser user = new MyUser();
                user.UserName = "admin@admin.se";
                user.Email = "admin@admin.se";
                user.FirstName = "Admin";
                user.LastName = "Admin";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
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
            };

            context.Events.AddRange(events);
            context.SaveChanges();
        }
    }
}
