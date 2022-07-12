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
    public class CustomersController : Controller
    {
        string Id = "Id";
        private readonly ModelContext _context;

        public CustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Customers.Include(c => c.Custrole);
            return View(await modelContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Custrole)
                .FirstOrDefaultAsync(m => m.Custid == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["Custroleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Custid,Custfname,Custlname,Custemail,Custroleid")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", customer.Custroleid);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["Custroleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", customer.Custroleid);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Custid,Custfname,Custlname,Custemail,Custroleid")] Customer customer)
        {
            if (id != customer.Custid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Custid))
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
            ViewData["Custroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", customer.Custroleid);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Custrole)
                .FirstOrDefaultAsync(m => m.Custid == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //GET Update Employee Profile..
        public async Task<IActionResult> UpdateProfile(decimal? id)
        {

            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            id = ViewBag.Id;
            if (id == null)
            {
                return NotFound();
            }
            //x = System.Convert.ToInt32(Id);
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(decimal id, [Bind("Custid,Custfname,Custlname,Custemail,Custroleid")] Customer customer)
        {
            if (id != customer.Custid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Custroleid = 2;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Custid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            ViewData["Custroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", customer.Custroleid);
            return View(customer);
        }





        //Show Profile...
        public async Task<IActionResult> Profile(decimal? id)
        {

            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            id = ViewBag.Id;
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);

        }



        public async Task<IActionResult> Invoice(decimal? id)
        {

            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            id = ViewBag.Id;
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Bills.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);

        }





        private bool CustomerExists(decimal id)
        {
            return _context.Customers.Any(e => e.Custid == id);
        }
    }
}
