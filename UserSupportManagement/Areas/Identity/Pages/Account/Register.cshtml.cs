using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using UserSupportManagement.Constants;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Areas.Identity.Pages.Account
{
    //[AllowAnonymous]
    [Authorize(Roles = "SuperAdmin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            //[Required]
            //[Display(Name = "Employee Name")]
            //public string EmployeeName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            //[Phone]
            //[Display(Name = "Phone Number")]
            //public string PhoneNumber { get; set; }

            //[Required]
            //[Display(Name = "Employee Code")]
            //public string EmployeeCode { get; set; }

            //[Display(Name = "Department")]
            //public string EmployeeDepartment { get; set; }

            //[Display(Name = "Designation")]
            //public string EmployeeDesignation { get; set; }

            //[Required]
            //[DataType(DataType.Date)]
            //[Display(Name = "Date of Birth")]
            //public DateTime EmployeeDOB { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Concern Name")]
            public int ConcernId { get; set; }

            //[Required]
            //[Display(Name = "Role Name")]
            //public string RoleId { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["concern"] = _context.Concerns.ToList();
            //ViewData["role"] = _context.Roles.ToList();

            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var concerns = _context.Concerns;
                var concern = concerns.FindAsync(Input.ConcernId).Result;

                // var role = _roleManager.FindByIdAsync(Input.RoleId).Result;

                //var user = new ApplicationUser
                //{
                //UserName = Input.Email,
                //Email = Input.Email,
                //EmployeeName = Input.EmployeeName,
                //EmployeeCode = Input.EmployeeCode,
                //EmployeeDOB = Input.EmployeeDOB,
                //EmployeeDepartment = Input.EmployeeDepartment,
                //EmployeeDesignation = Input.EmployeeDesignation,
                //PhoneNumber = Input.PhoneNumber,
                //ConcernId = concern.ConcernId};

                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    ConcernId = concern.ConcernId
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");
                    //await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    //return LocalRedirect(returnUrl);
                    //    //return RedirectToAction("Index", "UserRole");
                    //    return RedirectToAction("Index", "UserRoles" );
                    //}
                    return RedirectToAction("Index", "UserRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
