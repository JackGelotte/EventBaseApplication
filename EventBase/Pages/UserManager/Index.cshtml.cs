using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EventBase.Pages.UserManager
{
    [Authorize(Policy = "RequireAdminRole")]
    public class IndexModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;
        private readonly UserManager<MyUser> _userManager;

        public IndexModel(EventBase.Data.EventBaseContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<MyUser> MyUsers { get; set; }
        public MyUser MyUser { get; set; }
        public async Task OnGetAsync()
        {
            MyUsers = await _context.MyUsers.ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {

            MyUsers = await _context.MyUsers.ToListAsync();
            MyUser = await _context.MyUsers.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (MyUser != null)
            {
                if (!_userManager.IsInRoleAsync(MyUser, "Organizer").Result)
                {
                    await _userManager.AddToRoleAsync(MyUser, "Organizer");
                    await _userManager.RemoveFromRoleAsync(MyUser, "Member");
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/UserManager/Index");
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(MyUser, "Organizer");
                    await _userManager.AddToRoleAsync(MyUser, "Member");
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/UserManager/Index");
                }
            }
            return Page();
        }


    }
}


