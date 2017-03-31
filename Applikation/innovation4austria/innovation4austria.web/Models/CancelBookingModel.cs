using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CancelBookingModel
    {
        public int Id { get; set; }

        public string Room { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Price { get; set; }
    }
}