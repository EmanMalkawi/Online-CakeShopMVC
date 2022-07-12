using System;
using System.Collections.Generic;

#nullable disable

namespace Cake.Models
{
    public partial class Feedback
    {
        public decimal Feedbackid { get; set; }
        public decimal? Custid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public virtual Customer Cust { get; set; }
    }
}
