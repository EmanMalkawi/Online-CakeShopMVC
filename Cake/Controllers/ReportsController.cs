using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models;

namespace Cake.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ModelContext _context;

        public ReportsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Reports.Include(r => r.Order).Include(r => r.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Order)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid");
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reportid,Orderid,Productid,Productname,Startdate,Enddate")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", report.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", report.Productid);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", report.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", report.Productid);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Reportid,Orderid,Productid,Productname,Startdate,Enddate")] Report report)
        {
            if (id != report.Reportid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Reportid))
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
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", report.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", report.Productid);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Order)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.Reportid == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(decimal id)
        {
            return _context.Reports.Any(e => e.Reportid == id);
        }
    }
}
