using innovation4austria.dataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class RoomFilterModel
    {
        public List<facility> FacilityList { get; set; }

        public List<furnishment> FurnishmentList { get; set; }

        public List<SearchRoomModel> RoomList { get; set; }
    }
}