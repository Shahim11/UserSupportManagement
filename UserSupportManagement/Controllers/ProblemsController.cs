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

            if (User.IsInRole("SuperAdmin"))
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

        // GET: Problems/Details/5
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

        // GET: Problems/Edit/5
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

        // POST: Problems/Edit/5
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

        // GET: Problems/Delete/5
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

        // POST: Problems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problem = await _context.Problems.FindAsync(id);
            _context.Problems.Remove(problem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProblemExists(int id)
        {
            return _context.Problems.Any(e => e.ProblemId == id);
        }
    }
}
