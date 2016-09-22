using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class Role :  IdentityRole<string>
    {
        public string Description { get; set; }
    }
}
