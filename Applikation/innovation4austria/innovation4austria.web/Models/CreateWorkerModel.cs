using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CreateWorkerModel
    {
        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Firstname { get; set; }

        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Lastname { get; set; }

        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [EmailAddress(ErrorMessage = "Kein gültiges Email-Format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Email { get; set; }

        public int CompanyId { get; set; }

        public List<RoleDropdownModel> RoleList { get; set; }
    }
}