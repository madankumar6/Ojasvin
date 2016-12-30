using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Web.Services.Identity
{
    public class TraclerRBACIsBannedHandler : AuthorizationHandler<TrackerRBACRequirement>
    {
        public TraclerRBACIsBannedHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TrackerRBACRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsBannedFromTracker" && claim.Value =="True"))
            {
                //To confirm that the user is banned from accessing the application and other handlers will fail to authorize the user.
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
