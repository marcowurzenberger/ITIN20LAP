using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace innovation4austria.web.Models
{
    public class PaymentViewModel
    {
        private const int startYear = 17;
        private const int endYear = 27;

        [RegularExpression("[a-zäöüßA-ZÄÖÜ ]+", ErrorMessage = "Nur Buchstaben erlaubt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string CardOwner { get; set; }

        [RegularExpression("[0-9]+", ErrorMessage = "Nur Zahlen erlaubt")]
        [DataType(DataType.CreditCard)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public string CardNumber { get; set; }

        [RegularExpression("[0-9]{3}", ErrorMessage = "Nur 3 Zahlen erlaubt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public int CVV { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [Range(1, 12, ErrorMessage = "Nur Zahlen von 1 bis 12 erlaubt")]
        public short ValidUntilMonth { get; set; }

        [Range(startYear, endYear, ErrorMessage = "Nur Zahlen von 17 bis 27 erlaubt")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        public short ValidUntilYear { get; set; }

        public decimal Amount { get; set; }

        public int RoomId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}