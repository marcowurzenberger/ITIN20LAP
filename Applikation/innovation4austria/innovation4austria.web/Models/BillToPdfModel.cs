using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class BillToPdfModel
    {
        public int Id { get; set; }

        public DateTime Billdate { get; set; }

        public List<ViewBookingdetail> Bookingdetails { get; set; }

        public decimal TotalPrice { get; set; }
    }
}