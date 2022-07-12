using Cake.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cake.Controllers
{
    public class AdminController : Controller
    {
        string Id = "Id";
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment iwebhostenviroment;


        public AdminController(ModelContext context, IWebHostEnvironment _iwebhostenviroment)
        {
            _context = context;
            iwebhostenviroment = _iwebhostenviroment;

        }


        public IActionResult Index(DateTime fromDate, DateTime toDate)
        {
            ViewBag.Id = HttpContext.Session.GetInt32(Id);
            ViewBag.countOfProduct = _context.Products.Count();
            ViewBag.countOfCategory = _context.Categories.Count();
            ViewBag.countOfCustomer = _context.Customers.Count();
            ViewBag.countOfEmployee = _context.Employees.Count();

            var result = _context.Orders.Where(x => x.Oederdate >= fromDate && x.Oederdate <= toDate).ToList();
            return View(result);
        }


        public IActionResult SearchDate()
        {

            var result = _context.Orders.ToList();

            return View(result);
        }

        [HttpPost]
        public IActionResult SearchDate(DateTime fromDate, DateTime toDate)
        {
            var result = _context.Orders.Where(x => x.Oederdate >= fromDate && x.Oederdate <= toDate).ToList();
            return View(result);
        }











        //public IActionResult CreateDocument()
        //{
        //    PdfDocument document = new PdfDocument();
        //    PdfPage page = document.Pages.Add();

        //    //Create PDF graphics for the page
        //    PdfGraphics graphics = page.Graphics;

        //    //Set the standard font
        //    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);


            
        //    //Draw the text
        //    graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));

        //    //Saving the PDF to the MemoryStream
        //    MemoryStream stream = new MemoryStream();

        //    document.Save(stream);

        //    //Set the position as '0'.
        //    stream.Position = 0;

        //    //Download the PDF document in the browser
        //    FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

        //    fileStreamResult.FileDownloadName = "Sample.pdf";

        //    return fileStreamResult;
        //}



        //public IActionResult CreatePDF()
        //{
        //    var dtTo = DateTime.Now.Month;
        //    var dto = DateTime.Now.Day;
        //    var dtT = DateTime.Now.Year;

        //    var month = _context.Orders.Include(x => x.Product).Where(x => x.Oederdate.Value.Month == dtTo).ToList();

        //    var sum = 0;
        //    var salary = 0;

        //    List<Order> Final = new List<Order>();
        //    foreach(var item in month.GroupBy(x=>x.Orderid).Select(y=>y.First()))
        //    {
        //        var count = item.Product.Orders.Count;

           
        //        item.Product.Price = ((int)(item.Product.Price) * count);
        //        Final.Add(item);

        //        sum += (int)(item.Product.Price);  
        //    }

        //    var sal = _context.Employees.ToList();
        //    foreach(var sa in sal)
        //    {
        //        salary += (int) (sa.Salary);
        //    }

        //    ViewBag.sum = sum;
        //    ViewBag.salary = salary;
        //    sum = 0; salary = 0;

        //    ModelContext model = new ModelContext();

        //    return View();
        //}
    





private bool EmployeeExists(decimal id)
        {
            return _context.Employees.Any(e => e.Empid == id);
        }

    }
}
