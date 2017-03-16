using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class EditRoomModel
    {
        public int Id { get; set; }

        [RegularExpression("[A-Za-z0-9]+", ErrorMessage = "Nur Buchstaben und Zahlen erlaubt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Description { get; set; }

        [RegularExpression("[0-9]+", ErrorMessage = "Nur Zahlen erlaubt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public decimal Price { get; set; }

        public int FacilityId { get; set; }

        public List<int> FurnishmentIds { get; set; }

        public List<FacilityViewModel> Facilities { get; set; }

        public List<FurnishmentViewModel> Furnishments { get; set; }
    }
}