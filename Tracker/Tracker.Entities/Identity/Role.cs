using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class Role :  IdentityRole
    {
        public string Description { get; set; }
    }
}
