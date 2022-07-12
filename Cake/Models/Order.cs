using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Order
    {
        public Order()
        {
            Bills = new HashSet<Bill>();
            Carts = new HashSet<Cart>();
            Reports = new HashSet<Report>();
        }

        public decimal Orderid { get; set; }
        public decimal? Custid { get; set; }
        public decimal? Productid { get; set; }
        public string Productname { get; set; }
        public decimal? Price { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? Oederdate { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
