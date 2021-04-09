using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventBase.Security
{
    public class MyHandler : AuthorizationHandler<MyRequirements>
    {
        private EventBaseContext _context;
        private UserManager<MyUser> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        public MyHandler(
            EventBaseContext context,
            UserManager<MyUser> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyRequirements requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            string eventId = httpContext.Request.Query["id"].ToString();

            bool eventIdMatchesOrgId = await _context.Events.Where(e => e.ID == int.Parse(eventId)).Select(o => o.Organizer).AnyAsync(u => u.Id == _userManager.GetUserId(httpContext.User));

            if (eventIdMatchesOrgId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
