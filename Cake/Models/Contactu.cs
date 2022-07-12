using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Contactu
    {
        public decimal Contactid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public decimal? Shopid { get; set; }

        public virtual Cakeshop Shop { get; set; }
    }
}
