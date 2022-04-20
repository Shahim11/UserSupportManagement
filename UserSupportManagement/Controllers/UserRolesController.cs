using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using UserSupportManagement.Data;
using UserSupportManagement.Models;


namespace UserSupportManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Where(x => x.EmployeeName != "Super Admin").ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.EmployeeCode = user.EmployeeCode;
                thisViewModel.EmployeeName = user.EmployeeName;
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }

        // GET: UserRoles/Edit
        public async Task<IActionResult> Edit(string userId)
        {
            //ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            
           // var userRole = roles;
            //var role = _context.Roles.Where(a=>a.Id==userRole.);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new ManageUserRolesViewModel()
            {
                //Id = user.Id,
                //RoleId = role.Id,
                RoleName = roles.ToString(),
                EmployeeName = user.EmployeeName,
                EmployeeCode = user.EmployeeCode
            };

            ViewBag.RoleName = model.RoleName;
            ViewBag.EmpName = model.EmployeeName;
            ViewBag.EmpId = model.EmployeeCode;

            //ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", role.Id);

            return View(model);

        }

        // POST: UserRoles/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
    }
}