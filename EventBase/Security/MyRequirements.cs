using EventBase.Data;
using EventBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBase.Security
{
    public class OrganizerAndEventMatchRequirement : IAuthorizationRequirement
    {
    }
}
