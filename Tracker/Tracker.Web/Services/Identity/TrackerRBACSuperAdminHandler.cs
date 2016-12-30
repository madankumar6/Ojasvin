using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Web.Services.Identity
{
    public class TrackerRBACSuperAdminHandler : AuthorizationHandler<TrackerRBACRequirement>
    {
        public TrackerRBACSuperAdminHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TrackerRBACRequirement requirement)
        {
            if (context.User.IsInRole("SuperAdmin"))
            {
                context.Succeed(requirement);
            }

            bool hasPermission = true;// permissionRepository.CheckPermissionForUser(context.User, requirement.Permission);
            if (hasPermission)
            {
            }

            return Task.FromResult(0);
        }
    }
}
