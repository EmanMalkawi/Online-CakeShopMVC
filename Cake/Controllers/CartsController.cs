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
    public class CartsController : Controller
    {
        private readonly ModelContext _context;

        public CartsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Carts.Include(c => c.Cust).Include(c => c.Order);
            return View(await modelContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Cust)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.Cartid == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid");
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cartid,Custid,Orderid,Item,Price,Quantity")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", cart.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", cart.Orderid);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", cart.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", cart.Orderid);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Cartid,Custid,Orderid,Item,Price,Quantity")] Cart cart)
        {
            if (id != cart.Cartid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Cartid))
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", cart.Custid);
            ViewData["Orderid"] = new SelectList(_context.Orders, "Orderid", "Orderid", cart.Orderid);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Cust)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.Cartid == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //User Cart..
        public async Task<IActionResult> UserCart(int id)
        {
            var result = _context.Carts.Where(x => x.Custid == id).ToList();
            return View(result);
        }






        // GET: Carts/Delete
        public async Task<IActionResult> DeleteCart(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Cust)
                .Include(c => c.Order)
                .FirstOrDefaultAsync(m => m.Cartid == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete
        [HttpPost, ActionName("DeleteCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCart(decimal id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }





        private bool CartExists(decimal id)
        {
            return _context.Carts.Any(e => e.Cartid == id);
        }
    }
}
