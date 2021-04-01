﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventBase.Pages.Events
{   [Authorize(Policy="RequireAdminRole")]
    public class CreateModel : PageModel
    {
        private readonly EventBase.Data.EventBaseContext _context;

        public CreateModel(EventBase.Data.EventBaseContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
