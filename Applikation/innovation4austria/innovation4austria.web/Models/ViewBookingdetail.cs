using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class ViewBookingdetail
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Room { get; set; }
    }
}