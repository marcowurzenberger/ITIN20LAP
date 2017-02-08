using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CreateCompanyModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld!")]
        [StringLength(50, ErrorMessage = "Max. 50 Zeichen")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld!")]
        [StringLength(10, ErrorMessage = "Max. 10 Zeichen")]
        public string Zip { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld!")]
        [StringLength(50, ErrorMessage = "Max. 50 Zeichen")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld!")]
        [StringLength(50, ErrorMessage = "Max. 50 Zeichen")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld!")]
        [StringLength(10, ErrorMessage = "Max. 10 Zeichen")]
        public string Number { get; set; }

    }
}