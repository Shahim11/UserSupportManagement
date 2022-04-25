using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    public class SolutionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SolutionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Solutions
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("SuperAdmin"))
            {
                var applicationDbContext = _context.Solutions.Include(s => s.Problem).Include(s => s.StatusType)
                    .Where(x => x.IsDeleted == false);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Solutions.Include(s => s.Problem).Include(s => s.StatusType)
                    .Where(x => x.IsActive == true);
                return View(await applicationDbContext.ToListAsync());
            }
            
        }

        // GET: Solutions/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions
                .Include(s => s.Problem)
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(m => m.SolutionId == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Solutions/Create
        public IActionResult Create()
        {
            // var pid = Request.Query["id"];
            //var problem = _context.Problems.FirstOrDefault(x => x.ProblemId == Convert.ToInt32(pid));
            //ViewBag.ProblemName = problem.ProblemName;
            //ViewBag.ProblemId = problem.ProblemId;

            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes.Where(x => x.IsActive == true).Where(a => a.IsDeleted == false), "StatusTypeId", "StatusTypeName");

            return View();
        }

        // POST: Solutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolutionId,ProblemId,StatusTypeId,SolutionDetails,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Solution solution)
        {
            solution.IsActive = true;
            solution.IsDeleted = false;

            //var pid = Request.Query["problemId"];
            //solution.ProblemId = Convert.ToInt32(pid);


            if (ModelState.IsValid)
            {
                _context.Add(solution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName", solution.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", solution.StatusTypeId);
            return View(solution);
        }

        // GET: Solutions/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName", solution.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", solution.StatusTypeId);
            return View(solution);
        }

        // POST: Solutions/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolutionId,ProblemId,StatusTypeId,SolutionDetails,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Solution solution)
        {
            if (id != solution.SolutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionExists(solution.SolutionId))
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
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName", solution.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", solution.StatusTypeId);
            return View(solution);
        }

        // GET: Solutions/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions
                .Include(s => s.Problem)
                .Include(s => s.StatusType)
                .FirstOrDefaultAsync(m => m.SolutionId == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // POST: Solutions/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            solution.IsDeleted = true;
            //_context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolutionExists(int id)
        {
            return _context.Solutions.Any(e => e.SolutionId == id);
        }
    }
}
