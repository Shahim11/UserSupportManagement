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
    [Authorize(Roles = "SuperAdmin,Admin,Support")]
    public class StatusTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusTypes
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("SuperAdmin"))
            {
                return View(await _context.StatusTypes.Where(x => x.IsDeleted == false).ToListAsync());
            }
            else
            {
                return View(await _context.StatusTypes.Where(x => x.IsActive == true).ToListAsync());
            }
        }

        // GET: StatusTypes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusType = await _context.StatusTypes
                .FirstOrDefaultAsync(m => m.StatusTypeId == id);
            if (statusType == null)
            {
                return NotFound();
            }

            return View(statusType);
        }

        // GET: StatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusTypeId,StatusTypeName,StatusTypeValue,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] StatusType statusType)
        {
            statusType.IsActive = true;
            statusType.IsDeleted = false;

            if (ModelState.IsValid)
            {
                _context.Add(statusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusType);
        }

        // GET: StatusTypes/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusType = await _context.StatusTypes.FindAsync(id);
            if (statusType == null)
            {
                return NotFound();
            }
            return View(statusType);
        }

        // POST: StatusTypes/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusTypeId,StatusTypeName,StatusTypeValue,IsActive,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] StatusType statusType)
        {
            if (id != statusType.StatusTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusTypeExists(statusType.StatusTypeId))
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
            return View(statusType);
        }

        // GET: StatusTypes/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusType = await _context.StatusTypes
                .FirstOrDefaultAsync(m => m.StatusTypeId == id);
            if (statusType == null)
            {
                return NotFound();
            }

            return View(statusType);
        }

        // POST: StatusTypes/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.IsInRole("SuperAdmin"))
            {
                var statusType = await _context.StatusTypes.FindAsync(id);
                statusType.IsDeleted = true;
                //_context.StatusTypes.Remove(statusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var statusType = await _context.StatusTypes.FindAsync(id);
                statusType.IsActive = false;
                //_context.StatusTypes.Remove(statusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool StatusTypeExists(int id)
        {
            return _context.StatusTypes.Any(e => e.StatusTypeId == id);
        }
    }
}
