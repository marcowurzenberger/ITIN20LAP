using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class CreateFacilityModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "max. 10 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Zip { get; set; }

        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "max. 50 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Street { get; set; }

        [StringLength(10, ErrorMessage = "max. 10 Zeichen")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Number { get; set; }
    }
}