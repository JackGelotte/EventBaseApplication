using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Identity;

namespace EventBase.Pages.MyEvents
{
    public class IndexModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public IndexModel(EventBase.Data.EventBaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ICollection<Event> Event { get; set; }
        public IList<MyUser> Attendees { get; set; }
        public MyUser MyUser { get; set; }
        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var CurrentUser = await _context.MyUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (_userManager.IsInRoleAsync(CurrentUser, "Organizer").Result)
            {
                var MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u => u.HostedEvents).ThenInclude(e=>e.Attendees).FirstOrDefaultAsync();
                Event = MyUser.HostedEvents;

            }
            else
            {
                var MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u => u.JoinedEvents).FirstOrDefaultAsync();
                Event = MyUser.JoinedEvents;
            }
        }
        public async Task<IActionResult> OnPostAsync(int? id, string name)
        {
            var CurrentEvent = await _context.Events.Include(u => u.Attendees).Where(e => e.ID == id).FirstOrDefaultAsync();
            var user = _context.MyUsers.Where(u => u.UserName == name).FirstOrDefault();

            CurrentEvent.Attendees.Remove(user);
            CurrentEvent.SpotsAvailable++;
            _context.SaveChanges();

            var userId = _userManager.GetUserId(User);
            var CurrentUser = await _context.MyUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (_userManager.IsInRoleAsync(CurrentUser, "Organizer").Result)
            {
                var MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u => u.HostedEvents).ThenInclude(e => e.Attendees).FirstOrDefaultAsync();
                Event = MyUser.HostedEvents;

            }
            else
            {
                var MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u => u.JoinedEvents).FirstOrDefaultAsync();
                Event = MyUser.JoinedEvents;
            }
            return RedirectToPage("./Index");
        }
    }
}
