using Cake.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cake.Controllers
{
    public class AccountantController : Controller
    {
        string Id = "Id";
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment iwebhostenviroment;



        public AccountantController(ModelContext context, IWebHostEnvironment _iwebhostenviroment)
        {
            _context = context;
            iwebhostenviroment = _iwebhostenviroment;
            
        }

        public IActionResult Index()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            ViewBag.countOfOrders = _context.Orders.Count();
            ViewBag.countOfCustomer = _context.Customers.Count();

            return View();
        }

        // GET Customers Info
        public async Task<IActionResult> CustomerInfo()
        {
            var modelContext = _context.Customers.Include(c => c.Custrole);
            return View(await modelContext.ToListAsync());
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


        // GET Orders
        public async Task<IActionResult> Order()
        {
            var result = _context.Orders.ToList();
            return View(result);
        }


    }
}
