using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomListModel
    {
        public RoomFilterModel Filter { get; set; }

        public List<RoomViewModel> Rooms { get; set; }
    }
}