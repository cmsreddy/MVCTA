using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TAMVC.Models.DBModels;

namespace TAMVC.Models
{
    public class RegisterModel : TAC_User
    {        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare("Password", ErrorMessage = "Both passwords did not match")]
        public string RepeatPassword { get; set; }

        [Display(Name = "Terms and Conditions")]
        public bool TandC { get; set; }

        public string ErrorMessage { get; set; }

    }
}