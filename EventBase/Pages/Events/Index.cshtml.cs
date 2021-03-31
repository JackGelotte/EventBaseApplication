using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventBase.Data;
using EventBase.Models;

namespace EventBase.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;

        public IndexModel(EventBase.Data.EventBaseContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();
        }
    }
}
