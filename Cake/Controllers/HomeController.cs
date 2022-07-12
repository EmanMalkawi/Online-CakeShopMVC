using Cake.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Cake.Controllers
{
    public class HomeController : Controller
    {
        string Id = "Id";
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);

            var item1 = _context.Homes.ToList();
            var item2 = _context.Categories.ToList();
            var item3 = _context.Cakeshops.ToList();
            var item4 = _context.Homes.ToList();
            var item5 = _context.Aboutus.ToList();
            var item6 = _context.Feedbacks.ToList();

            var result = Tuple.Create<IEnumerable< Cake.Models.Home >, IEnumerable<Cake.Models.Category>, IEnumerable<Cake.Models.Cakeshop>, IEnumerable<Cake.Models.Home>, IEnumerable<Cake.Models.Aboutu>, IEnumerable<Cake.Models.Feedback>>
                (item1, item2, item3, item4, item5,item6);
            return View(result);
        }


        public IActionResult OurProducts(int id)
        {
            var availableRooms = _context.Products.Where(x => x.Productcategid == id).ToList();

            return View(availableRooms);
        }


        public IActionResult Aboutus()
        {

            ViewBag.Id = HttpContext.Session.GetInt32(Id);

            var item1 = _context.Homes.ToList();
            var item2 = _context.Categories.ToList();
            var item3 = _context.Cakeshops.ToList();
            var item4 = _context.Homes.ToList();
            var item5 = _context.Aboutus.ToList();
            var item6 = _context.Feedbacks.ToList();

            var result = Tuple.Create<IEnumerable<Cake.Models.Home>, IEnumerable<Cake.Models.Category>, IEnumerable<Cake.Models.Cakeshop>, IEnumerable<Cake.Models.Home>, IEnumerable<Cake.Models.Aboutu>, IEnumerable<Cake.Models.Feedback>>
                (item1, item2, item3, item4, item5, item6);
            return View(result);
        }


        public IActionResult Shop()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            var item = _context.Products.ToList();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Shop([Bind("Cartid,Custid,Orderid,Item,Price,Quantity")] Cart cart, string proName ,int? price, int? Qty)
        {

            if (ModelState.IsValid)
            {

                ViewBag.Id = HttpContext.Session.GetInt32(Id);
                if (ViewBag.Id == null)
                {

                    return RedirectToAction("Signin", "SigninRegister");
                }

                if (!string.IsNullOrEmpty(proName) && price != null && Qty != null)
                {
                    cart.Item = proName;
                    cart.Price = price;
                    cart.Quantity = Qty;
                    cart.Custid = ViewBag.Id;
                    _context.Add(cart);
                  await _context.SaveChangesAsync();
                   return RedirectToAction("Shop", "Home");

                }
               
            }

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Testimonial()
        {
            //var result = _context.Feedbacks.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Testimonial([Bind("Feedbackid,Custid,Firstname,Lastname,Email,Text")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {

                ViewBag.Id = HttpContext.Session.GetInt32(Id);
                if (ViewBag.Id == null)
                {

                    return RedirectToAction("Signin", "SigninRegister");
                }

                feedback.Custid = ViewBag.Id;
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Testimonial", "Home");
            }

            return RedirectToAction("Testimonial", "Home");

        }




        [HttpGet]
        public IActionResult ContactUs()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs([Bind("Contactid,Firstname,,Lastname,Email,Message")] Contactu contactu)
        {
            if (ModelState.IsValid)
            {

                //ViewBag.Id = HttpContext.Session.GetInt32(Id);
                //if (ViewBag.Id == null)
                //{

                //    return RedirectToAction("Signin", "SigninRegister");
                //}

                //contactu.Custid = ViewBag.Id;
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction("ContactUs", "Home");
            }

            return RedirectToAction("ContactUs", "Home");

        }



        [HttpGet]
        public IActionResult Cart()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            var result = _context.Carts.ToList();
            return View(result);
        }





        public IActionResult Orders()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            //var item = _context.Orders.ToList();

            return View(/*item*/);
        }

        [HttpPost]
        public async Task<IActionResult> Orders([Bind("Orderid,Custid,Productid,Productname,Price,Quantity,Oederdate")] Order order, string proName, int? price, int? Qty, DateTime orderDate)
        {

            if (ModelState.IsValid)
            {

                ViewBag.Id = HttpContext.Session.GetInt32(Id);
                if (ViewBag.Id == null)
                {

                    return RedirectToAction("Signin", "SigninRegister");
                }

                if (!string.IsNullOrEmpty(proName) && price != null && Qty != null && orderDate != null)
                {
                    order.Productname = proName;
                    order.Price = price * Qty;
                    order.Quantity = Qty;
                    order.Oederdate = orderDate;

                    order.Custid = ViewBag.Id;
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");

                }


            }

            return RedirectToAction("Shop", "Home");
        }
















        //commentedd

        public IActionResult Cakes()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            var item = _context.Products.ToList();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Cakes([Bind("Orderid,Custid,Productid,Productname,Price,Quantity,Oederdate")] Order order, int Proid)
        {

            if (ModelState.IsValid)
            {

                ViewBag.Id = HttpContext.Session.GetInt32(Id);
                if (ViewBag.Id == null)
                {

                    return RedirectToAction("Signin", "SigninRegister");
                }

                order.Custid = ViewBag.Id;
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Cakes", "Home");
            }

            return RedirectToAction("Index", "Home");
        }




        public IActionResult Order()
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            //var item = _context.Orders.ToList();

            return View(/*item*/);
        }

        [HttpPost]
        public async Task<IActionResult> Order([Bind("Cartid,Custid,Orderid,Item,Price,Quantity")] Cart cart, int Proid)
        {

            if (ModelState.IsValid)
            {

                ViewBag.Id = HttpContext.Session.GetInt32(Id);
                if (ViewBag.Id == null)
                {

                    return RedirectToAction("Signin", "SigninRegister");
                }
                
                cart.Custid = ViewBag.Id;
                cart.Price =cart.Price * cart.Quantity;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Cakes", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        ///



























        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
