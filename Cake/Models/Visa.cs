using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Visa
    {
        public decimal Visaid { get; set; }
        public decimal? Balance { get; set; }
        public string Email { get; set; }
        public decimal? Custid { get; set; }

        public virtual Customer Cust { get; set; }
    }
}
