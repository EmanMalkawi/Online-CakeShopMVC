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
    public class BillsController : Controller
    {
        private readonly ModelContext _context;

        public BillsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Bills.Include(b => b.Cust).Include(b => b.Order).Include(b => b.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Cust)
                .Include(b => b.Order)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Billid == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid");
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid");
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Billid,Custid,Orderid,Productid,Productname,Orderdate,Price")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", bill.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", bill.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", bill.Productid);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", bill.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", bill.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", bill.Productid);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Billid,Custid,Orderid,Productid,Productname,Orderdate,Price")] Bill bill)
        {
            if (id != bill.Billid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Billid))
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", bill.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", bill.Orderid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", bill.Productid);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Cust)
                .Include(b => b.Order)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Billid == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var bill = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(decimal id)
        {
            return _context.Bills.Any(e => e.Billid == id);
        }
    }
}
