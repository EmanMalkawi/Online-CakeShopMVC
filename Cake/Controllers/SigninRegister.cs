using Cake.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cake.Controllers
{
    public class SigninRegister : Controller
    {
        const string Id = "Id";
        private readonly ModelContext _context;

        public SigninRegister(ModelContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }


        //Register Function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Custid", "Custfname", "Custlname", "Custemail")] Customer customer, string email, string password)
        {
            if (ModelState.IsValid)
            {
                customer.Custroleid = 2;
                _context.Add(customer);
                await _context.SaveChangesAsync();

                Login login = new Login();
                login.Username = customer.Custemail;
                login.Password = password;
                login.Loginroleid = 2;
                login.Custid = customer.Custid;
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Signin", "SigninRegister");
            }
            return View();
        }


        //Login Function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signin([Bind("Username", "Password")] Login login)
        {


            var Auth = _context.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).SingleOrDefault();
            if (Auth != null)
            {
                // id == 0 admain 
                // id == 1 accountant 
                // id == 2 customer

                switch (Auth.Loginroleid)
                {
                    case 0: {
                            HttpContext.Session.SetInt32(Id, Convert.ToInt32(Auth.Empid.Value));
                            return RedirectToAction("Index", "Admin"); 
                            }
                    case 1:
                        {
                            HttpContext.Session.SetInt32(Id, Convert.ToInt32(Auth.Empid.Value));
                            return RedirectToAction("Index", "Accountant"); 
                        }
                    case 2: { 
                            HttpContext.Session.SetInt32(Id, Convert.ToInt32(Auth.Custid.Value) ); 
                            return RedirectToAction("Index", "Home"); 
                        }
                    case null:
                        {
                            return RedirectToAction("Signin", "SigninRegister");
                        }
                }
            }

            return View();
        }


        public async Task<IActionResult> Logout()
        {

            //ViewBag.Id = null;
            return RedirectToAction("Signin", "SigninRegister");
            //return RedirectToAction("Index", "Home");

        }

    }
}
