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

namespace EventBase.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DetailsModel(EventBase.Data.EventBaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }
        public MyUser MyUser { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);

            var userId = _userManager.GetUserId(User);

            MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u => u.JoinedEvents).FirstOrDefaultAsync();

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            Event = await _context.Events.FirstOrDefaultAsync(m => m.ID == id);

            var userId = _userManager.GetUserId(User);

            MyUser = await _context.MyUsers.Where(u => u.Id == userId).Include(u=>u.JoinedEvents).FirstOrDefaultAsync();

            if (MyUser == null)
            {
                return NotFound();
            }
            else
            {
                Event.SpotsAvailable --;
                MyUser.JoinedEvents.Add(Event);
                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}
