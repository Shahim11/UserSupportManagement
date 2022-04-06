using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserSupportManagement.Constants;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    //[Authorize(Roles = "SuperAdmin")]
    public class ConcernsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConcernsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Concerns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Concerns.Where(x => x.IsDeleted == false).ToListAsync());
        }

        // GET: Concerns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concern = await _context.Concerns
                .FirstOrDefaultAsync(m => m.ConcernId == id);
            if (concern == null)
            {
                return NotFound();
            }

            return View(concern);
        }

        // GET: Concerns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concerns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcernId,ConcernName,ConcernShortName,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Concern concern)
        {
            concern.IsActive = true;
            concern.IsDeleted = false;

            if (ModelState.IsValid)
            {
                _context.Add(concern);
                await _context.SaveChangesAsync();
                TempData["success"] = "Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(concern);
        }

        // GET: Concerns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concern = await _context.Concerns.FindAsync(id);
            if (concern == null)
            {
                return NotFound();
            }
            return View(concern);
        }

        // POST: Concerns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConcernId,ConcernName,ConcernShortName,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Concern concern)
        {
            if (id != concern.ConcernId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concern);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcernExists(concern.ConcernId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(concern);
        }

        // GET: Concerns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concern = await _context.Concerns
                .FirstOrDefaultAsync(m => m.ConcernId == id);
            if (concern == null)
            {
                return NotFound();
            }

            return View(concern);
        }

        // POST: Concerns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concern = await _context.Concerns.FindAsync(id);
            _context.Concerns.Remove(concern);
            await _context.SaveChangesAsync();
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ConcernExists(int id)
        {
            return _context.Concerns.Any(e => e.ConcernId == id);
        }
    }
}
