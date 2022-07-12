using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Cart
    {
        public decimal Cartid { get; set; }
        public decimal? Custid { get; set; }
        public decimal? Orderid { get; set; }
        public string Item { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Order Order { get; set; }
    }
}
