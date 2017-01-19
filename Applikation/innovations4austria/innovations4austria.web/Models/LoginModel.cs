using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovations4austria.web.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [EmailAddress(ErrorMessage = "Kein gültiges Email-Format")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool StayLoggedIn { get; set; }
    }
}