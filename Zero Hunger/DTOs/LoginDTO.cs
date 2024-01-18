using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.DTOs
{
    public class LoginDTO:ValidationAttribute
    {

        [Required(ErrorMessage = "Username is required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
    }
}