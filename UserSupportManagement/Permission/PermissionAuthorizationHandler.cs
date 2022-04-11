﻿using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using UserSupportManagement.Permission;

namespace UserSupportManagement
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {

        public PermissionAuthorizationHandler()
        {

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                              x.Value == requirement.Permission &&
                                                              x.Issuer == "LOCAL AUTHORITY");
            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}