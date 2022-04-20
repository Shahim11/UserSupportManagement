using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserAccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        // GET: User/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(o => o.Concern)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            //var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            //var userRoles = await userManager.GetRolesAsync(user);

            var model = new UserAccountViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                ConcernId = user.ConcernId,
                EmployeeName = user.EmployeeName,
                EmployeeCode = user.EmployeeCode,
                EmployeeDOB = user.EmployeeDOB,
                EmployeeDepartment = user.EmployeeDepartment,
                EmployeeDesignation = user.EmployeeDesignation,
                PhoneNumber = user.PhoneNumber
            };
            ViewData["ConcernId"] = new SelectList(_context.Concerns, "ConcernId", "ConcernName", user.ConcernId);

            return View(model);
        }

        // POST: User/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserAccountViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.ConcernId = model.ConcernId;
                user.EmployeeName = model.EmployeeName;
                user.EmployeeCode = model.EmployeeCode;
                user.EmployeeDOB = model.EmployeeDOB;
                user.EmployeeDepartment = model.EmployeeDepartment;
                user.EmployeeDesignation = model.EmployeeDesignation;
                user.PhoneNumber = model.PhoneNumber;
                user.IsActive = model.IsActive;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "UserRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewData["ConcernId"] = new SelectList(_context.Concerns, "ConcernId", "ConcernName", model.ConcernId);

                return View(model);
            }
        }

        

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
