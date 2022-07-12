using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cake.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Cake.Controllers
{
    public class HomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment iwebhostenviroment;


        public HomesController(ModelContext context, IWebHostEnvironment _iwebhostenviroment)
        {
            _context = context;
            iwebhostenviroment = _iwebhostenviroment;
        }

        // GET: Homes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Homes.Include(h => h.Shop);
            return View(await modelContext.ToListAsync());
        }

        // GET: Homes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.Shop)
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: Homes/Create
        public IActionResult Create()
        {
            ViewData["Shopid"] = new SelectList(_context.Cakeshops, "Cakeshopid", "Cakeshopname");
            return View();
        }

        // POST: Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homeid,Imagepath,Backgroundtext,Aboutustext,Shopid,ImageFile")] Home home)
        {
            if (ModelState.IsValid)
            {
                if (home.ImageFile != null)
                {
                    string wwwRootPath = iwebhostenviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await home.ImageFile.CopyToAsync(filestream);
                    }

                    home.Imagepath = fileName;
                   _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
 
            }
            ViewData["Shopid"] = new SelectList(_context.Cakeshops, "Cakeshopid", "Cakeshopid", home.Shopid);
            return View(home);
        }

        // GET: Homes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            ViewData["Shopid"] = new SelectList(_context.Cakeshops, "Cakeshopid", "Cakeshopname", home.Shopid);
            return View(home);
        }

        // POST: Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homeid,Imagepath,Backgroundtext,Aboutustext,Shopid,ImageFile")] Home home)
        {
            if (id != home.Homeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (home.ImageFile != null)
                    {
                        string wwwRootPath = iwebhostenviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + home.ImageFile.FileName;

                        string extension = Path.GetExtension(home.ImageFile.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await home.ImageFile.CopyToAsync(filestream);
                        }

                        home.Imagepath = fileName;

                    }
                    _context.Update(home);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.Homeid))
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
            ViewData["Shopid"] = new SelectList(_context.Cakeshops, "Cakeshopid", "Cakeshopid", home.Shopid);
            return View(home);
        }

        // GET: Homes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .Include(h => h.Shop)
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var home = await _context.Homes.FindAsync(id);
            _context.Homes.Remove(home);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
            return _context.Homes.Any(e => e.Homeid == id);
        }
    }
}
