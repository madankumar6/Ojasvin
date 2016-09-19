using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities.Identity
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();



    }
}
