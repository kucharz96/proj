using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace Projekt1.Models.VM.Account
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Pole jest puste")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pole jest puste")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}