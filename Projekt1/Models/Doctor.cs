using Projekt1.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt1.Models
{
    public class Doctor : User
    {
        

        public virtual Specialization Specializations { get; set; }
        public int PWZ { get; set; }

    }
}