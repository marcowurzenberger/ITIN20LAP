using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class ViewFacilityModel
    {
        public CreateFacilityModel Create { get; set; }

        public List<ListFacilityModel> Facilities { get; set; }
    }
}