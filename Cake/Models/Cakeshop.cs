using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Cakeshop
    {
        public Cakeshop()
        {
            Aboutus = new HashSet<Aboutu>();
            Contactus = new HashSet<Contactu>();
            Homes = new HashSet<Home>();
        }

        public decimal Cakeshopid { get; set; }
        public string Cakeshopname { get; set; }

        public virtual ICollection<Aboutu> Aboutus { get; set; }
        public virtual ICollection<Contactu> Contactus { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
    }
}
