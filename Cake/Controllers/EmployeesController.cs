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
using Microsoft.AspNetCore.Http;

namespace Cake.Controllers
{
    public class EmployeesController : Controller
    {
        string Id = "Id";
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment iwebhostenviroment;


        public EmployeesController(ModelContext context, IWebHostEnvironment _iwebhostenviroment)
        {
            _context = context;
            iwebhostenviroment = _iwebhostenviroment;

        }


        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Employees.Include(e => e.Position);
            return View(await modelContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["Positionid"] = new SelectList(_context.Roles, "Roleid", "Rolename");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Empid,Fname,Lname,Email,Salary,Imagepath,Positionname,Positionid,ImageFile")] Employee employee, string epassword)
        {
            if (ModelState.IsValid)
            {

                //Login login = new Login();
                //login.Empid = employee.Empid;
                //login.Username = employee.Email;
                //login.Password = password;
                //login.Loginroleid = 1;
                //_context.Add(login);
                //await _context.SaveChangesAsync();

                if (employee.ImageFile != null)
                {
                    string wwwRootPath = iwebhostenviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + employee.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await employee.ImageFile.CopyToAsync(filestream);
                    }

                    employee.Imagepath = fileName;
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

                var eid = _context.Employees.OrderByDescending(x => x.Empid).FirstOrDefault().Empid;

                if (!string.IsNullOrEmpty(epassword))
                {
                Login login = new Login();
                login.Username = employee.Email;
                login.Password = epassword;
                login.Loginroleid = 1;
                login.Empid = eid;

                _context.Add(login);
                await _context.SaveChangesAsync();
                }
               
            }



            ViewData["Positionid"] = new SelectList(_context.Roles, "Roleid", "Roleid", employee.Positionid);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Positionid"] = new SelectList(_context.Roles, "Roleid", "Rolename", employee.Positionid);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Empid,Fname,Lname,Email,Salary,Imagepath,Positionname,Positionid,ImageFile")] Employee employee)
        {
            if (id != employee.Empid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (employee.ImageFile != null)
                    {
                        string wwwRootPath = iwebhostenviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + employee.ImageFile.FileName;

                        string extension = Path.GetExtension(employee.ImageFile.FileName);

                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await employee.ImageFile.CopyToAsync(filestream);
                        }

                        employee.Imagepath = fileName;

                    }
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Empid))
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
            ViewData["Positionid"] = new SelectList(_context.Roles, "Roleid", "Roleid", employee.Positionid);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Empid == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
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
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(decimal id, [Bind("Empid,Fname,Lname,Email,Salary,Imagepath,Positionname,Positionid,ImageFile")] Employee employee)
        {
            if (id != employee.Empid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = iwebhostenviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + employee.ImageFile.FileName;


                    string extension = Path.GetExtension(employee.ImageFile.FileName);

                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await employee.ImageFile.CopyToAsync(filestream);
                    }

                    employee.Imagepath = fileName;
                    employee.Positionid = 0;
                    employee.Positionname = "Admin";
                    employee.Salary = 2000;
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Empid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Admin");
            }
            return View(employee);
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
            //x = System.Convert.ToInt32(Id);
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }



            private bool EmployeeExists(decimal id)
            {
            return _context.Employees.Any(e => e.Empid == id);
            }
    }
}
