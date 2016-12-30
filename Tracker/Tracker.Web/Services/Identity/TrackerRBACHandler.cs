using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Entities.Identity;

namespace Tracker.Web.Services.Identity
{
    public class TrackerRBACHandler : AuthorizationHandler<TrackerRBACRequirement>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public TrackerRBACHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TrackerRBACRequirement requirement)
        {
            if (context.User == null)
            {
                // no user authorized. Alternatively call context.Fail() to ensure a failure as another handler for this requirement may succeed
                context.Fail();
                return null;
            }

            bool hasPermission = true;// permissionRepository.CheckPermissionForUser(context.User, requirement.Permission);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
