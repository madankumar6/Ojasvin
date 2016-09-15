using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class UserRole : IdentityUserRole<string>
    {
    }
}
