using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Kein gültiges Email-Format")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Passwort zu kurz")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string Password { get; set; }

        public bool StayLoggedIn { get; set; }
    }
}