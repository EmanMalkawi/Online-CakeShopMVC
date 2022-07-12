using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Aboutu
    {
        public decimal Aboutusid { get; set; }
        public string Mobilenumber { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Location { get; set; }
        public decimal? Shopid { get; set; }

        public virtual Cakeshop Shop { get; set; }
    }
}
