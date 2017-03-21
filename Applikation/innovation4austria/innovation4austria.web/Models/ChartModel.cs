using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class ChartModel
    {
        public decimal Expenditure { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public Color CompanyColor { get; set; }

        public List<ViewCompanyModel> FilterCompanies { get; set; }
    }
}