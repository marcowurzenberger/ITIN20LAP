using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class DashboardRoomModel
    {
        public int Room_Id { get; set; }

        public string RoomDescription { get; set; }

        public int Booking_Id { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Enddate { get; set; }

        public bool Canceled { get; set; }
    }
}