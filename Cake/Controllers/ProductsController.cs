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
    public class ProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment iwebhostenviroment;


        public ProductsController(ModelContext context, IWebHostEnvironment _iwebhostenviroment)
        {
            _context = context;
            iwebhostenviroment = _iwebhostenviroment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Products.Include(p => p.Productcateg);
            return View(await modelContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Productcateg)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Productcategid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Productdescription,Imagepath,Productcategid,ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwRootPath = iwebhostenviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(filestream);
                    }

                    product.Imagepath = fileName;
                    _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }

            }
            ViewData["Productcategid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid", product.Productcategid);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Productcategid"] = new SelectList(_context.Categories, "Categoryid", "Categoryname", product.Productcategid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,Price,Productdescription,Imagepath,Productcategid,ImageFile")] Product product)
        {
            if (id != product.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {
                        string wwwRootPath = iwebhostenviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;

                        string extension = Path.GetExtension(product.ImageFile.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(filestream);
                        }

                        product.Imagepath = fileName;
                    }
                        _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Productid))
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
            ViewData["Productcategid"] = new SelectList(_context.Categories, "Categoryid", "Categoryid", product.Productcategid);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Productcateg)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Search...
        public IActionResult SearchName()
        {
            var result = _context.Products.ToList();

            return View(result);
        }

        [HttpPost]
        public IActionResult SearchName(string word)
        {
            var result = _context.Products.Where(x => x.Productname.Contains(word)).ToList();

            return View(result);
        }







        private bool ProductExists(decimal id)
        {
            return _context.Products.Any(e => e.Productid == id);
        }
    }
}
