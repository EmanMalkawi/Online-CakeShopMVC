﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models;

namespace Cake.Controllers
{
    public class VisasController : Controller
    {
        private readonly ModelContext _context;

        public VisasController(ModelContext context)
        {
            _context = context;
        }

        // GET: Visas
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Visas.Include(v => v.Cust);
            return View(await modelContext.ToListAsync());
        }

        // GET: Visas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .Include(v => v.Cust)
                .FirstOrDefaultAsync(m => m.Visaid == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // GET: Visas/Create
        public IActionResult Create()
        {
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custemail");
            return View();
        }

        // POST: Visas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visaid,Balance,Email,Custid")] Visa visa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", visa.Custid);
            return View(visa);
        }

        // GET: Visas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custemail", visa.Custid);
            return View(visa);
        }

        // POST: Visas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Visaid,Balance,Email,Custid")] Visa visa)
        {
            if (id != visa.Visaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisaExists(visa.Visaid))
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
            ViewData["Custid"] = new SelectList(_context.Customers, "Custid", "Custid", visa.Custid);
            return View(visa);
        }

        // GET: Visas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visa = await _context.Visas
                .Include(v => v.Cust)
                .FirstOrDefaultAsync(m => m.Visaid == id);
            if (visa == null)
            {
                return NotFound();
            }

            return View(visa);
        }

        // POST: Visas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var visa = await _context.Visas.FindAsync(id);
            _context.Visas.Remove(visa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisaExists(decimal id)
        {
            return _context.Visas.Any(e => e.Visaid == id);
        }
    }
}
