using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    //[Authorize]
    public class ProblemsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProblemsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Problems
        public async Task<IActionResult> Index()
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            
            var problem = _context.Problems.ToList();

            var user = _context.Users.ToList();

            foreach (var prob in problem)
            {
                var created = prob.CreatedBy;
                var users = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == created);
                
                var empCode = users.EmployeeCode;

                prob.CreatedBy = empCode;
            }

            if(User.IsInRole("SuperAdmin") || User.IsInRole("Admin") || User.IsInRole("Support") || User.IsInRole("SupplyChain"))
            {
                var applicationDbContext = _context.Problems.Include(p => p.ProblemType).Include(p => p.StatusType).Where(x => x.IsDeleted == false);
                
                return View(await applicationDbContext.ToListAsync());
            }

            else
            {
                var applicationDbContext = _context.Problems.Include(p => p.ProblemType).Include(p => p.StatusType).Where(x => x.IsDeleted == false).Where(o => o.CreatedBy == User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
                return View(await applicationDbContext.ToListAsync());
            }

        }

        // GET: Problems/Filter
        public async Task<IActionResult> ProblemFilter()
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var problem = _context.Problems.ToList();

            var user = _context.Users.ToList();

            foreach (var prob in problem)
            {
                var created = prob.CreatedBy;
                var users = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == created);

                var empCode = users.EmployeeCode;

                prob.CreatedBy = empCode;
            }

            var applicationDbContext = _context.Problems.Include(p => p.ProblemType).Include(p => p.StatusType).Where(x => x.IsDeleted == false).Where(o => o.CreatedBy == User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Problems/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var problem = await _context.Problems
                .Include(p => p.ProblemType)
                .Include(p => p.StatusType)
                .FirstOrDefaultAsync(m => m.ProblemId == id);

            var solution = _context.Solutions.FirstOrDefault(x => x.ProblemId == problem.ProblemId);

            //var solution = _context.Solutions.FirstOrDefault(x => x.ProblemId == problem.ProblemId);

            var order = _context.Orders.FirstOrDefault(x => x.ProblemId == problem.ProblemId);

            if (problem == null)
            {
                return NotFound();
            }
            
            var solutionViewModel = new SolutionViewModel();
            solutionViewModel.ProblemId = problem.ProblemId;
            solutionViewModel.ProblemName = problem.ProblemName;
            solutionViewModel.ProblemTypeName = problem.ProblemType.ProblemTypeName;
            solutionViewModel.StatusTypeName = problem.StatusType.StatusTypeName;
            solutionViewModel.ProblemDetails = problem.ProblemDetails;
            if (solution != null)
            {
                solutionViewModel.SolutionDetails = solution.SolutionDetails;
                solutionViewModel.orderNeeded = solution.orderNeeded;
            }

            if (order != null)
            {
                solutionViewModel.OrderId = order.OrderId;
            }

            //else
            //{
            //    solutionViewModel.SolutionDetails = "Solution Isn't Created Yet!";
            //}

            solutionViewModel.CreatedBy = problem.CreatedBy;
            solutionViewModel.CreatedDate = problem.CreatedDate;
            solutionViewModel.ModifiedBy = problem.ModifiedBy;
            solutionViewModel.ModifiedDate = problem.ModifiedDate;
            solutionViewModel.IsActive = problem.IsActive;
            solutionViewModel.IsDeleted = problem.IsDeleted;

            //solutionViewModel.orderNeeded = solution.orderNeeded;

            TempData["ProblemId"] = solutionViewModel.ProblemId; 

            return View(solutionViewModel);

        }

        // GET: Problems/Create
        public IActionResult Create()
        {
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "ProblemTypeId", "ProblemTypeName");
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName");
            return View();
        }

        // POST: Problems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProblemId,ProblemTypeId,StatusTypeId,ProblemName,ProblemDetails,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Problem problem)
        {
            problem.IsActive = true;
            problem.IsDeleted = false;
            problem.StatusTypeId = 1;

            if (ModelState.IsValid)
            {
                _context.Add(problem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "ProblemTypeId", "ProblemTypeName", problem.ProblemTypeId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", problem.StatusTypeId);
            return View(problem);
        }

        // GET: Solutions/Create
        public IActionResult SolutionCreate()
        {
            // var pid = Request.Query["id"];
            //var problem = _context.Problems.FirstOrDefault(x => x.ProblemId == Convert.ToInt32(pid));
            //ViewBag.ProblemName = problem.ProblemName;
            //ViewBag.ProblemId = problem.ProblemId;

            int ProblemId = Convert.ToInt32(TempData["ProblemId"]);
            ViewBag.ProblemId = ProblemId;

            var problem = _context.Problems.FirstOrDefault(x => x.ProblemId == ProblemId);
            ViewBag.ProblemName = problem.ProblemName;

            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes.Where(x => x.IsActive == true).Where(a => a.IsDeleted == false), "StatusTypeId", "StatusTypeName");

            return View();
        }

        // POST: Solutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolutionCreate([Bind("SolutionId,ProblemId,StatusTypeId,SolutionDetails,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy,orderNeeded")] Solution solution)
        {
            solution.IsActive = true;
            solution.IsDeleted = false;

            int ProblemId = Convert.ToInt32(TempData["ProblemId"]);
            //var pid = Request.Query["problemId"];
            solution.ProblemId = ProblemId;


            if (ModelState.IsValid)
            {
                _context.Add(solution);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Solutions");
            }

            //ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName", solution.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", solution.StatusTypeId);
            return View(solution);
        }

        // GET: Orders/Create
        public IActionResult OrderCreate()
        {
            int ProblemId = Convert.ToInt32(TempData["ProblemId"]);
            ViewBag.ProblemId = ProblemId;

            var problem = _context.Problems.FirstOrDefault(x => x.ProblemId == ProblemId);
            ViewBag.ProblemName = problem.ProblemName;

            //ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName");
            
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderCreate([Bind("OrderId,VendorId,StatusTypeId,ProblemId,OrderName,OrderItemName,OrderItemDetails,OrderItemQuantity,IsActive,IsDeleted,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Order order)
        {
            order.IsActive = true;
            order.IsDeleted = false;

            int ProblemId = Convert.ToInt32(TempData["ProblemId"]);
            //var pid = Request.Query["problemId"];
            order.ProblemId = ProblemId;

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Orders");
            }
            //ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemId", order.ProblemId);
            
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeId", order.StatusTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorId", order.VendorId);
            return View(order);
        }


        // GET: Problems/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "ProblemTypeId", "ProblemTypeName", problem.ProblemTypeId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusType", problem.StatusTypeId);
            return View(problem);
        }

        // POST: Problems/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProblemId,ProblemTypeId,StatusTypeId,ProblemName,ProblemDetails,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Problem problem)
        {
            problem.StatusTypeId = 1;

            //var temp = _context.Problems.Find(id);
            //problem.StatusTypeId = temp.StatusTypeId;
            //await _context.SaveChangesAsync();

            if (id != problem.ProblemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(problem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemExists(problem.ProblemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProblemTypeId"] = new SelectList(_context.ProblemTypes, "ProblemTypeId", "ProblemTypeId", problem.ProblemTypeId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeId", problem.StatusTypeId);
            return View(problem);
        }

        // GET: Problems/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problem = await _context.Problems
                .Include(p => p.ProblemType)
                .Include(p => p.StatusType)
                .FirstOrDefaultAsync(m => m.ProblemId == id);
            if (problem == null)
            {
                return NotFound();
            }

            return View(problem);
        }

        // POST: Problems/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            problem.IsDeleted = true;
            //_context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.ProblemId == id);
        }
    }
}
