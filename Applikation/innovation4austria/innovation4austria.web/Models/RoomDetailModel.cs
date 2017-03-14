using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomDetailModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int BookingId { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Enddate { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal PricePerDay { get; set; }

        public int BookedDays { get; set; }
    }
}