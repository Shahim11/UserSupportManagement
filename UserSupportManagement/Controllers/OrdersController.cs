using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserSupportManagement.Data;
using UserSupportManagement.Models;

namespace UserSupportManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Support")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Problem).Include(o => o.StatusType).Include(o => o.Vendor).Where(x=>x.IsDeleted==false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Problem)
                .Include(o => o.StatusType)
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.OrderId == id && m.IsDeleted == false );
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName");
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,VendorId,StatusTypeId,ProblemId,OrderName,OrderItemName,OrderItemDetails,OrderItemQuantity,IsActive,IsDeleted,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Order order)
        {
            order.IsActive = true;
            order.IsDeleted = false;

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemId", order.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeId", order.StatusTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorId", order.VendorId);
            return View(order);
        }

        // GET: Orders/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemName", order.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeName", order.StatusTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName", order.VendorId);
            return View(order);
        }

        // POST: Orders/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,VendorId,StatusTypeId,ProblemId,OrderName,OrderItemName,OrderItemDetails,OrderItemQuantity,IsActive,IsDeleted,CreatedDate,ModifiedDate,CreatedBy,ModifiedBy")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["ProblemId"] = new SelectList(_context.Problems, "ProblemId", "ProblemId", order.ProblemId);
            ViewData["StatusTypeId"] = new SelectList(_context.StatusTypes, "StatusTypeId", "StatusTypeId", order.StatusTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorId", order.VendorId);
            return View(order);
        }

        // GET: Orders/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Problem)
                .Include(o => o.StatusType)
                .Include(o => o.Vendor)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.IsDeleted = true;
            //_context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
