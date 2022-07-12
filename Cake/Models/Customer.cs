using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
            Carts = new HashSet<Cart>();
            Feedbacks = new HashSet<Feedback>();
            Logins = new HashSet<Login>();
            Orders = new HashSet<Order>();
            Visas = new HashSet<Visa>();
        }

        public decimal Custid { get; set; }
        public string Custfname { get; set; }
        public string Custlname { get; set; }
        public string Custemail { get; set; }
        public decimal? Custroleid { get; set; }

        public virtual Role Custrole { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Visa> Visas { get; set; }
    }
}
