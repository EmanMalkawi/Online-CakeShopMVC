using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Cake.Models
{
    public partial class Home
    {
        public decimal Homeid { get; set; }
        public string Imagepath { get; set; }
        public string Backgroundtext { get; set; }
        public string Aboutustext { get; set; }
        public decimal? Shopid { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual Cakeshop Shop { get; set; }
    }
}
