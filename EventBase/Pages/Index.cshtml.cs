using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBase.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<MyUser> _signInManager;

        public IndexModel(ILogger<IndexModel> logger, SignInManager<MyUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!EventBaseContext.Grabs)
            {
                await _signInManager.SignOutAsync();
                EventBaseContext.Grabs = true;
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
