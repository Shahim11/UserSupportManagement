using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UserSupportManagement.Constants;
using UserSupportManagement.Models;

namespace UserSupportManagement.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Support.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.SupplyChain.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}