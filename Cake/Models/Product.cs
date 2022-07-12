using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Cake.Models
{
    public partial class Product
    {
        public Product()
        {
            Bills = new HashSet<Bill>();
            Orders = new HashSet<Order>();
            Reports = new HashSet<Report>();
        }

        public decimal Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Price { get; set; }
        public string Productdescription { get; set; }
        public string Imagepath { get; set; }
        public decimal? Productcategid { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }


        public virtual Category Productcateg { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
