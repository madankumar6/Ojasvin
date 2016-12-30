namespace Tracker.Entities.Identity
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Status { get; set; }
        public DateTime LastModified { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
