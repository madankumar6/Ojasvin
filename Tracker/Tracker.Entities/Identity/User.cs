using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Tracker.Entities
{
    public class User : IdentityUser
    {
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }
}
