using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class Dashboardi4aModel
    {
        public List<ManageCompanyModel> ManageCompanies { get; set; }

        public CreateCompanyModel CreateCompany { get; set; }
    }
}