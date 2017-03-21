using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class BannerModel
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public decimal Price { get; set; }

        public string Facility { get; set; }
    }
}