using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Login
    {
        public decimal Loginid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal? Loginroleid { get; set; }
        public decimal? Empid { get; set; }
        public decimal? Custid { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Employee Emp { get; set; }
        public virtual Role Loginrole { get; set; }
    }
}
