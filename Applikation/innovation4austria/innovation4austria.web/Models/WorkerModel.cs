using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class WorkerModel
    {
        public int CompanyId { get; set; }

        public List<ViewWorkerModel> Workers { get; set; }
    }
}