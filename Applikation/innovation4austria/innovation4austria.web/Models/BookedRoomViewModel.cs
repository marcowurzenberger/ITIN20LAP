using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class BookedRoomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public string Facility { get; set; }

        public int FacilityId { get; set; }

        public int DateDiff { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<FurnishmentViewModel> Furnishments { get; set; }
    }
}