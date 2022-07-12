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
    public class CakeshopsController : Controller
    {
        private readonly ModelContext _context;

        public CakeshopsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Cakeshops
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cakeshops.ToListAsync());
        }

        // GET: Cakeshops/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeshop = await _context.Cakeshops
                .FirstOrDefaultAsync(m => m.Cakeshopid == id);
            if (cakeshop == null)
            {
                return NotFound();
            }

            return View(cakeshop);
        }

        // GET: Cakeshops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cakeshops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cakeshopid,Cakeshopname")] Cakeshop cakeshop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cakeshop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cakeshop);
        }

        // GET: Cakeshops/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeshop = await _context.Cakeshops.FindAsync(id);
            if (cakeshop == null)
            {
                return NotFound();
            }
            return View(cakeshop);
        }

        // POST: Cakeshops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Cakeshopid,Cakeshopname")] Cakeshop cakeshop)
        {
            if (id != cakeshop.Cakeshopid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cakeshop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CakeshopExists(cakeshop.Cakeshopid))
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
            return View(cakeshop);
        }

        // GET: Cakeshops/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cakeshop = await _context.Cakeshops
                .FirstOrDefaultAsync(m => m.Cakeshopid == id);
            if (cakeshop == null)
            {
                return NotFound();
            }

            return View(cakeshop);
        }

        // POST: Cakeshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var cakeshop = await _context.Cakeshops.FindAsync(id);
            _context.Cakeshops.Remove(cakeshop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CakeshopExists(decimal id)
        {
            return _context.Cakeshops.Any(e => e.Cakeshopid == id);
        }
    }
}
