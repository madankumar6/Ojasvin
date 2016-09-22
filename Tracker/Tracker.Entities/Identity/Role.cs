using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class Role :  IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
