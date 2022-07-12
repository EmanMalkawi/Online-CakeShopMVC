using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Bill
    {
        public decimal Billid { get; set; }
        public decimal? Custid { get; set; }
        public decimal? Orderid { get; set; }
        public decimal? Productid { get; set; }
        public string Productname { get; set; }
        public DateTime? Orderdate { get; set; }
        public decimal? Price { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
