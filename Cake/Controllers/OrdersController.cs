using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models;
using Microsoft.AspNetCore.Http;

namespace Cake.Controllers
{
    public class OrdersController : Controller
    {
        string Id = "Id";
        private readonly ModelContext _context;

        public OrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Orders.Include(o => o.Cust).Include(o => o.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Cust)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custemail");
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Custid,Productid,Productname,Price,Quantity,Oederdate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", order.Custid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", order.Productid);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", order.Custid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", order.Productid);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Custid,Productid,Productname,Price,Quantity,Oederdate")] Order order)
        {
            if (id != order.Orderid)
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
                    if (!OrderExists(order.Orderid))
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", order.Custid);
            ViewData["Productid"] = new SelectList(_context.Products, "Productid", "Productid", order.Productid);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Cust)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult SearchDate()
        {
            var result = _context.Orders.ToList();

            return View(result);
        }

        [HttpPost]
        public IActionResult SearchDate(DateTime startDate, DateTime EndDate)
        {
            var result = _context.Orders.Where(x => x.Oederdate>= startDate || x.Oederdate <= EndDate).ToList();

            return View(result);
        }



        // GET: Customer Orders
        public async Task<IActionResult> CustOrder( int id)
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            if (ViewBag.Id == null)
            {

                return RedirectToAction("Signin", "SigninRegister");
            }

            var result = _context.Orders.Where(x=>x.Custid == id);
            return View(result);
        }




        private bool OrderExists(decimal id)
        {
            return _context.Orders.Any(e => e.Orderid == id);
        }
    }
}
