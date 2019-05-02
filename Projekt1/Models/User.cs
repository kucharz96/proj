using Projekt1.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.Models
{
   
    public abstract class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsEnable { get; set; }
        public Guid UserKey { get; set; }
       
        
    }
}