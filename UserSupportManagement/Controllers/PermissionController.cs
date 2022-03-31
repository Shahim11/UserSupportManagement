﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSupportManagement.Constants;
using UserSupportManagement.Helpers;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();

            allPermissions.GetPermissions(typeof(Permissions.Concerns), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Vendors), roleId);
            allPermissions.GetPermissions(typeof(Permissions.ProblemTypes), roleId);
            allPermissions.GetPermissions(typeof(Permissions.StatusTypes), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Problems), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Solutions), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Orders), roleId);

            var role = await _roleManager.FindByIdAsync(roleId);
            model.Name = role.Name;
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }

        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            return RedirectToAction("Index","Roles",new { roleId = model.RoleId });
        }
    }
}
