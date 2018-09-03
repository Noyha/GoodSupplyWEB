using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoodSupplyWEB.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא נוכחית")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "הסיסמא החדשה חייבת להכיל לפחות {2} תווים.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמא חדשה")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "וידוא סיסמא חדשה")]
        [Compare("NewPassword", ErrorMessage = "וידוא סיסמא אינו תואם לסיסמא החדשה, נסה שוב!")]
        public string ConfirmPassword { get; set; }
    }
}