using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserSupportManagement.Constants;
using UserSupportManagement.Models;

namespace UserSupportManagement.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "basicuser@gmail.com",
                Email = "basicuser@gmail.com",
                EmployeeName = "Basic User",
                EmployeeCode = "B1720586",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                ConcernId = 1,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                EmployeeName = "Super Admin",
                EmployeeCode = "SA1720586",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                ConcernId = 1,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Support.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SupplyChain.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Concerns, Vendors,ProblemTypes,StatusTypes, Problems,Solutions,Orders");
            //await roleManager.AddPermissionClaim(adminRole, "Vendors");
            //await roleManager.AddPermissionClaim(adminRole, "ProblemTypes");
            //await roleManager.AddPermissionClaim(adminRole, "StatusTypes");
            //await roleManager.AddPermissionClaim(adminRole, "Problems");
            //await roleManager.AddPermissionClaim(adminRole, "Solutions");
            //await roleManager.AddPermissionClaim(adminRole, "Orders");
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
