//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;
//using UserSupportManagement.Models;

//namespace UserSupportManagement.Controllers
//{
//    [Authorize(Roles = "SuperAdmin,Admin")]
//    public class UsersController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        public UsersController(UserManager<ApplicationUser> userManager)
//        {
//            _userManager = userManager;
//        }
//        public async Task<IActionResult> Index()
//        {
//            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
//            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).Where(x => x.EmployeeName != "Super Admin").ToListAsync();
//            return View(allUsersExceptCurrentUser);
//        }
//    }
//}