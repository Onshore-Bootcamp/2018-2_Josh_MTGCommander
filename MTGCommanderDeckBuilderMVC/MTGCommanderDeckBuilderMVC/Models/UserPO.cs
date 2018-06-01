using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MTGCommanderDeckBuilderMVC.Models
{
    public class UserPO
    {
        //Declaring all object properties for a User
        public long UserID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A Username must be between 5 and 30 characters long!")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,30}$",ErrorMessage ="That password is invalid!")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "You're first name is too long!")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "You're last name is to long!")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(150, ErrorMessage = "That email is too long!")]
        [EmailAddress(ErrorMessage = "That is not a valid email address!")]
        public string EmailAddress { get; set; }

        public int RoleID { get; set; }
    }
}