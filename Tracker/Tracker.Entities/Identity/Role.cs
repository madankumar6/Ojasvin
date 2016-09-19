using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class Role :  IdentityRole
    {
        public string Description { get; set; }
    }
}
