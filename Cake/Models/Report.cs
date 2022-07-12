using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Report
    {
        public decimal Reportid { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Productid { get; set; }
        public string Productname { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
