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

namespace EventBase.Pages.UserManager
{   [Authorize(Policy = "RequireAdminRole")]
    public class IndexModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;

        public IndexModel(EventBase.Data.EventBaseContext context)
        {
            _context = context;
        }

        public IList<MyUser> MyUsers { get;set; }

        public async Task OnGetAsync()
        {
            MyUsers = await _context.MyUsers.ToListAsync();
        }
    }
}
