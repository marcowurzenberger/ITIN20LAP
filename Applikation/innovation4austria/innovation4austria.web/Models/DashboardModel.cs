using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class DashboardModel
    {
        public List<DashboardUserModel> Users { get; set; }

        public List<DashboardBillModel> Bills { get; set; }

        public List<DashboardRoomModel> Rooms { get; set; }
    }
}