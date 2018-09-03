using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ClientViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "דוא\"ל")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמא חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [Display(Name = "סיסמא")] 
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "וידוא סיסמא")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "וידוא סיסמא אינו תואם לסיסמא, נסה שוב!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "מספר פלאפון")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "שם העסק")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "כתובת העסק")]
        public string BusinessAddress { get; set; }

        [Required]
        [Display(Name = "ע.מ/ח\"פ")]
        public string VATIdNumber { get; set; }
    }
}