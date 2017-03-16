using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class BookedRoomListModel
    {
        public BookedRoomFilterModel Filter { get; set; }

        public List<BookedRoomViewModel> Rooms { get; set; }
    }
}