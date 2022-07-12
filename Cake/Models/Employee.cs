using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Cake.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Logins = new HashSet<Login>();
        }

        public decimal Empid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public int? Salary { get; set; }
        public string Imagepath { get; set; }
        public string Positionname { get; set; }
        public decimal? Positionid { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        public virtual Role Position { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
