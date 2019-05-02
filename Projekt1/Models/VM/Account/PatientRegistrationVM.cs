using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt1.Models.VM.Account
{
    public class PatientRegistrationVM
    {

        public string FirstName { get; set; }
       
        public string LastName { get; set; }


     
        [EmailAddress]
        public string Email { get; set; }
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
   
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string SamePassword { get; set; }
       

    }
}