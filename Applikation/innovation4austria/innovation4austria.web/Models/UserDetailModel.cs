using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class UserDetailModel
    {
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [RegularExpression("[A-Za-zÄÖÜäöüß]+", ErrorMessage = "Vorname darf nur Buchstaben enthalten")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [RegularExpression("[A-Za-zÄÖÜäöüß]+", ErrorMessage = "Nachname darf nur Buchstaben enthalten")]
        public string Lastname { get; set; }

        public string Role { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwörter stimmen nicht überein!")]
        [DataType(DataType.Password)]
        public string ReEnterNewPassword { get; set; }
    }
}