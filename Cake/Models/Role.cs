using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Role
    {
        public Role()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            Logins = new HashSet<Login>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
