namespace Tracker.Entities.Identity
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class User : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
