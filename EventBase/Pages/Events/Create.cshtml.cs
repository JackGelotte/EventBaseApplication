using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventBase.Pages.Events
{   [Authorize(Policy="RequireOrganizerRole")]
    public class CreateModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public CreateModel(EventBase.Data.EventBaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }
        public MyUser CurrentUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = _userManager.GetUserId(User);

            CurrentUser = await _context.MyUsers.Where(u => u.Id == userId).FirstOrDefaultAsync();

            Event.Organizer = CurrentUser;
            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
