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
    public class LoginsController : Controller
    {
        private readonly ModelContext _context;

        public LoginsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Logins.Include(l => l.Cust).Include(l => l.Emp).Include(l => l.Loginrole);
            return View(await modelContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Cust)
                .Include(l => l.Emp)
                .Include(l => l.Loginrole)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            //ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custemail");
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Email");
            ViewData["Loginroleid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Loginid,Username,Password,Loginroleid,Empid,Custid")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", login.Custid);
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", login.Empid);
            ViewData["Loginroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Loginroleid);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", login.Custid);
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", login.Empid);
            ViewData["Loginroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Loginroleid);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Loginid,Username,Password,Loginroleid,Empid,Custid")] Login login)
        {
            if (id != login.Loginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Loginid))
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", login.Custid);
            ViewData["Empid"] = new SelectList(_context.Employees, "Empid", "Empid", login.Empid);
            ViewData["Loginroleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", login.Loginroleid);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .Include(l => l.Cust)
                .Include(l => l.Emp)
                .Include(l => l.Loginrole)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var login = await _context.Logins.FindAsync(id);
            _context.Logins.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(decimal id)
        {
            return _context.Logins.Any(e => e.Loginid == id);
        }
    }
}
