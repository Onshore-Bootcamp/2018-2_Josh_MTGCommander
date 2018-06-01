using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MTGCommanderDeckBuilderMVC.Models
{
    public class LoginPO
    {
        //Declaring all object properties for a Login
        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid username!")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid password!")]
        public string Password { get; set; }
    }
}