using innovation4austria.dataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class FilterFurnishmentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public image Image { get; set; }
    }
}