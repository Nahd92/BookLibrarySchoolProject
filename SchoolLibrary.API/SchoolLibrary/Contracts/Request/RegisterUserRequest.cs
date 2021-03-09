using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolLibrary.Contracts.Request
{
    public class RegisterUserRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "the {0} must be atleast {5} Characters.", MinimumLength = 5 )]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [StringLength(100, ErrorMessage = "the {0} must be atleast {5} Characters.", MinimumLength = 5)]
        public string ConfirmPassword { get; set; }
    }
}