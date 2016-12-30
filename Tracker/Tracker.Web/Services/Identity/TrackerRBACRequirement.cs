using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Web.Services.Identity
{
    public class TrackerRBACRequirement : IAuthorizationRequirement
    {
        public TrackerRBACRequirement()
        {

        }
    }
}
