using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class BookedRoomFilterModel
    {
        public DateTime FilterDate { get; set; }

        public List<ViewCompanyModel> FilterCompanies { get; set; }
    }
}