using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomFilterModel
    {
        //public int FurnishmentId { get; set; }

        public List<FilterFurnishmentModel> Furnishments { get; set; }

        public DateTime FilterDate { get; set; }
    }
}